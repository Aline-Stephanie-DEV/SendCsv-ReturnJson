# Sistema de conversão de registros de ponto

Projeto desenvolvido durante o processo de seleção da Auvo Sistemas.  

A aplicação realiza a leitura dos arquivos na pasta selecionada pelo usuário, colhendo os seus dados, tratando-os e retorna as informações sobre os gastos dos departamentos, bem como as informações de cada funcionário que pertence ao departamento, em um arquivo único em formato json.

## Detalhes da implementação: 
#### 1. O usuário informa a pasta, com arquivos .csv, e faz o upload.
#### 2. A controller Home é chamada e executa uma função. Nela: 
- Lê todos os arquivos.  
- Trata os dados de cada arquivo, pegando as seguintes informações:

```
> Nome do arquivo: obtém o nome do departamento e a data vigente (dia, mês e ano).     
> Dados do arquivo: identifica onde está cada coluna e faz a leitura de todas as linhas.
```
- Com as informações coletadas, os dados são validados.
- Após a validação, realizamos os cálculos:

```
> Pega a lista de pontos e o departamento.   
> Avalia quantos dias úteis o mês vigente possui.    
> Seleciona todos os registros de cada funcionário.  
> Analisa os dados do funcionário em cada registro, para calcular dias e horas extras ou faltantes.  
> Calcula os valores referentes a cada funcionário a partir das informações geradas.   
> Gera as informações de gasto do departamento.  
> Repete a ação para todos os arquivos, gerando todas as informações de gasto.  
```
- Salva as informações de cada arquivo.
- Gera arquivo único com todas as informações, em formato json.
- Retorna o arquivo para o usuário.
