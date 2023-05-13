# ChallengeB3
Projeto de desafio porposto pelo time desenvolvimento da B3.

Projeto se propoe a fazer um crud com patterns de mercado e mecanismos usados em soluções no mercado de capitais, 
como mesageria e bibliotecas de compressão de dados para camada de transporte como protobuf.

Arquitetura - Message/Event Driven e DDD
Command-> Event
Query-> Reply
Usando  Filas do RabbiMQ para coreografia do ambiente.

DDD - modelagem de software que segue um conjunto de práticas com 
objetivo de facilitar a implementação de complexas regras e processos de negócios que tratamos como domínio.

Padrões Criacionais usados:
Factory
Singleton

Padrões Comportmentais
Command
Mediator 

Mais
Ioc - Inversão de controle
CQRS - com Coreografia
Injeção de depedencia
Unit of Work
Event Sourcing
Alguns conceitos de solid tbm foram usados.

Rodar o docker do rabbitMQ

docker run -d --hostname rabbitserver --name rabbitmq-server -p 15672:15672 -p 5672:5672 rabbitmq:3-management


![image](https://github.com/bvarandas/ChallengeB3/assets/13907905/ce855aa1-63b9-45f5-9b43-12ea7ad95afb)

![image](https://github.com/bvarandas/ChallengeB3/assets/13907905/4a69d490-3858-40e6-aaa6-1ec93e4b503e)

![image](https://github.com/bvarandas/ChallengeB3/assets/13907905/69ecb338-71f1-402b-a888-ca63a7b3055a)



