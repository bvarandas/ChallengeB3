using ChallengeB3.Domain.Events;
using ChallengeB3.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeB3.Application.EventSourceNormalizes;

public class RegisterHistory
{
    public static IList<RegisterHistoryData> HistoryData { get; set; } = null!;

    public static IList<RegisterHistoryData> ToJavaScriptRegisterHistory(IList<StoredEvent> storedEvents)
    {
        HistoryData = new List<RegisterHistoryData>();
        RegisterHistoryDeserializer(storedEvents);

        var sorted = HistoryData.OrderBy(c => c.When);
        var list = new List<RegisterHistoryData>();
        var last = new RegisterHistoryData();

        foreach (var change in sorted)
        {
            var jsSlot = new RegisterHistoryData
            {
                RegisterId = change.RegisterId == string.Empty || change.RegisterId == last.RegisterId? "": change.RegisterId,
                Description = string.IsNullOrWhiteSpace(change.Description) || change.Description == last.Description ? "" : change.Description,
                Status = string.IsNullOrWhiteSpace(change.Status) || change.Status == last.Status ? "" : change.Status,
                Date = change.Date,
                Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                When = change.When,
                Who = change.Who
            };

            list.Add(jsSlot);
            last = change;
        }
        return list;
    }

    private static void RegisterHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
    {
        foreach (var e in storedEvents)
        {
            var slot = new RegisterHistoryData();
            dynamic values;

            switch (e.MessageType)
            {
                case "RegisterInsertedEvent":
                    values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                    slot.Description = values["Description"];
                    slot.Status = values["Status"];
                    slot.Date = values["Date"];
                    slot.Action = "Inserted";
                    slot.When = values["Timestamp"];
                    slot.RegisterId = values["RegisterId"];
                    slot.Who = e.User;
                    break;
                case "RegisterUpdatedEvent":
                    values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                    slot.Description = values["Description"];
                    slot.Status = values["Status"];
                    slot.Date = values["Date"];
                    slot.Action = "Updated";
                    slot.When = values["Timestamp"];
                    slot.RegisterId = values["RegisterId"];
                    slot.Who = e.User;
                    break;
                case "RegisterRemovedEvent":
                    values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                    slot.Action = "Removed";
                    slot.When = values["Timestamp"];
                    slot.RegisterId = values["RegisterId"];
                    slot.Who = e.User;
                    break;
            }
            HistoryData.Add(slot);
        }
    }
}