# Desafio Inoa

Esse repositório contém o desafio realizado no processo seletivo para a empresa INOA. Ele consiste em um programa em C# que notifica via email quando uma ação está dentro do range de compra ou de venda.

### 📋 Pré-requisitos

Este programa foi feito para rodar no sistema operacional Windows.

### 🔧 Instalação

Esse projeto possui dependências, para instala-las, rode o seguinte comando:

bash
dotnet build


### Configuração

Dentro do Projeto existe um arquivo `App.config`. Nele deve-se ser colocado as informações referentes ao servidor de email, assim como o email de destino das notificações.

### Execução

Para executa-lo, rode o seguinte comando:

bash
dotnet run $TICKER $SELL_PRICE $BUY_PRICE