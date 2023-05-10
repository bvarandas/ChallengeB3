# ChallengeB3
Projeto de desafio porposto pelo time desenvolvimento da B3.

Projeto se propoe a fazer umcrud com patterns de mercado e mecanismos usados em soluções no mercado de capitais.

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
Alguns conceitos de solid tbm foram usados.
