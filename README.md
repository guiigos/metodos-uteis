# MetodosUteis
:octopus:  DLL de métodos uteis em projetos .net

##Funções
###[Certificado](Funcoes/Classes/Certificado.cs)
- **AssinarXML** - Assinar XML
- **AssinarXmlPorElemento** - Assinar XML por elemento
- **X509CertificateToBase64** - Retornar certificado base 64
- **EncryptMessage** - Criptografat mensagem com certificado
- **DescryptMessage** - Descriptografar mensagem com certificado
- **RetornarThumbprint** - Retornar certificado por thumbprint
- **RetornarNome** - Retornar certificado por nome
- **Vencido** - Verificar validade do certificado
- **SelecionarPorNome** - Selecionar um certificado por nome
- **SelecionarPorThumbprint** - Selecionar um certificado por thumbprint
- **Base64ToX509Certificate** - Transformar certificado base 64 em X509Certificate2

###[Criptografia](Funcoes/Classes/Criptografia.cs)
- [x] **EncryptSHA** - Criptografar texto em SHA1
- [x] **EncryptASCHII** - Criptografar texto em ASCII
- [x] **DecryptASCHII** - Descriptografar texto em ASCII
- [x] **EncryptMD5** - Criptografar texto em MD5
- [x] **EncryptPass** - Criptografar texto utilizando palavra passe
- [x] **DecryptPass** - Descriptografar texto utilizando palavra passe
- [x] **EncryptAES** - Criptografar texto em AES
- [x] **DecryptAES** - Descriptografar texto em AES

###[CriptografiaAes](Funcoes/Classes/CriptografiaAes.cs)
- Classe de criptografia AES

###[CriptografiaCryptoJs](Funcoes/Classes/CriptografiaCryptoJs.cs)
- Classe de criptografia baseada no CryptoJs utilizando o formato AES sendo equivalenta a realizada no JavaScript

###[BuscaCep](Funcoes/Classes/BuscaCep.cs)
- [x] **ViaCEP** - Busca de CEP baseada nos serviços da ViaCEP (https://viacep.com.br)

###[DataBase](Funcoes/Classes/DataBase.cs)
- [x] **Sum** - Método que realiza o SUM de uma coluna
- [x] **Max** - Método que realiza o MAX de uma coluna
- [x] **Min** - Método que realiza o MIN de uma coluna
- [x] **Avg** - Método que realiza o AVG de uma coluna
- [x] **Count** - Método que realiza o COUNT de uma coluna
- [x] **Update** - Método que realiza o UPDATE de um registro
- [x] **Delete** - Método que realiza o DELETE de um registro
- [x] **Select** - Método que realiza o SELECT de um registro retornando um Object
- [x] **Select** - Método que realiza o SELECT de um registro retornando um DataTable
- [x] **Select** - Método que realiza o SELECT de um registro retornando um DataRowCollection
- [x] **ColunasTabela** - Método que realiza o SELECT de uma coluna da tabela retornando uma lista de valores
- [x] **CreateParametersFromString** - Método que criar os parametros e concactena os seus valores em suas respectivas condições
- [x] **ExisteParametro** - Método que verifica se o existe algum parametro já adicionado no arry de parametros
- [x] **MontaSQL** - Método que cria uma String SQL pra INSERT ou UPDATE
- [x] **PreencheObjetos** - Método responsável por preencher o objeto com um SqlDataReader
- [x] **PreencheObjetos** - Método responsável por preencher o objeto com um DataRow
- [x] **AddParametros** - Método responsável por criar os parametros de um sqlCommand
- [x] **ContraSqlInjection** - Método que trata ataques de SQL injection

###[Email](Funcoes/Classes/Email.cs)
- [x] **EnviarEmail** - Classe para realização de envio de e-mails

###[Json](Funcoes/Classes/Json.cs)
- [x] **DataTabelToJSON** - Converte um DataTable em uma String JSON

###[Xml](Funcoes/Classes/Xml.cs)
- [x] **StringToXmlDocument** - Converte uma STRING em um XmlDocument
- [x] **ObjectToXmlDocument** - Converte um OBJETO em um XmlDocument
- [x] **StringToObject** - Converte uma String XML em um Objeto
- [x] **XmlDocumentToString** - Converte um XmlDocument para uma String XML
- [x] **ObjectToStringXML** - Converte um Objeto para uma String XML
- [x] **ValidarDocumentoXml** - Realiza a validação de um XML conforme sua XSD

###[Server](Funcoes/Classes/Server.cs)
- [x] **MontaSOAP** - Método que realiza a montagem de um envelope SOAP
- [x] **UploadArquivoFTP** - Realiza o UPLOAD de arquivos para um servidor FT

###[UtilDate](Funcoes/Classes/UtilDate.cs)
- [x] **DiferencaDias** - Retorna a diferença entre dias
- [x] **TruncarData** - Truca um DateTime
- [x] **DataExenso** - Retorna a data por extenso
- [x] **DiaSemana** - Retorna o dia da semana
- [x] **ZerarTime** - Zera o time da data

###[UtilString](Funcoes/Classes/UtilString.cs)
- [x] **SubstringRight** - Realiza o Substring pela direita
- [x] **RemoveEspeciais** - Remove caracteres especiais
- [x] **RemoveAcentos** - Remove acentos
- [x] **RemoverCaracteresEspeciais** - Remove caracteres especiais
- [x] **SoNumeros** - Retorna somente os números do texto
- [x] **SoLetras** - Retorna somente as letras do texto
- [x] **AcertoDG** - Realiza o acerto DG
- [x] **ValorExtenso** - Retorna um valor por extenso

###[Validators](Funcoes/Classes/Validators.cs)
- [x] **ValidaEnderecoEmail** - Método que realiza a validação de um endereço de Email
- [x] **ValidaCnpj** - Método que realiza a validação de um CNPJ
- [x] **ValidaCpf** - Método que realiza a validação de um CPF
- [x] **ValidaPis** - Método que realiza a validação de um PIS

###[DefaultValues - Extensões de valores default](Funcoes/Values/DefaultValues.cs)
###[ComplementValues - Extensões de complementos](Funcoes/Values/ComplementValues.cs)

##Utilização

```
Funcoes.[CLASSE].[METODO]
```

**Atenção:**
Quaisquer dúvidas a respeito do uso desta biblioteca, abra um novo Issue!

## Licença
Projeto desenvolvido para fins acadêmicos.
[MIT License](./LICENSE)
