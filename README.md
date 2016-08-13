# MetodosUteis
:octopus:  DLL de métodos uteis em projetos .net

##Funções
###[Certificado](Funcoes/Classes/Certificado.cs)
- [x] **AssinarXML** - Assinar XML
- [x] **AssinarXmlPorElemento** - Assinar XML por elemento
- [x] **X509CertificateToBase64** - Retornar certificado base 64
- [x] **EncryptMessage** - Criptografat mensagem com certificado
- [x] **DescryptMessage** - Descriptografar mensagem com certificado
- [x] **RetornarThumbprint** - Retornar certificado por thumbprint
- [x] **RetornarNome** - Retornar certificado por nome
- [x] **Vencido** - Verificar validade do certificado
- [x] **SelecionarPorNome** - Selecionar um certificado por nome
- [x] **SelecionarPorThumbprint** - Selecionar um certificado por thumbprint
- [x] **Base64ToX509Certificate** - Transformar certificado base 64 em X509Certificate2

###[Criptografia](Funcoes/Classes/Criptografia.cs)
- [x] **EncryptSHA** - Criptografar texto em SHA1
- [x] **EncryptASCHII** - Criptografar texto em ASCII
- [x] **DecryptASCHII** - Descriptografar texto em ASCII
- [x] **EncryptMD5** - Criptografar texto em MD5
- [x] **EncryptPass** - Criptografar texto utilizando palavra passe
- [x] **DecryptPass** - Descriptografar texto utilizando palavra passe
- [x] **EncryptAES** - Criptografar texto em AES
- [x] **DecryptAES** - Descriptografar texto em AES

###[DataBase](Funcoes/Classes/DataBase.cs)
- [x] **Sum** - Retorna o SUM
- [x] **Max** - Retorna o MAX
- [x] **Min** - Retorna o MIN
- [x] **Avg** - Retorna o AVG
- [x] **Count** - Retorna o COUNT
- [x] **Update** - Realiza uma alteração UPDATE
- [x] **Select** - Realiza uma consulta SELECT retornando um Object
- [x] **Select** - Realiza uma consulta SELECT retornando um DataTable
- [x] **Select** - Realiza uma consulta SELECT retornando um DataRowCollection
- [x] **Select** - Realiza uma consulta SELECT retornando um List
- [x] **ColunasTabela** - Retorna as colunas de uma tabela
- [x] **MontaSql** - Monta o SQL utilizando a tabela
- [x] **AddParametros** - Adiciona os parâmetros a um SqlCommand conforme o objeto
- [x] **PreencheObjetos** - Preenche o objeto utilizando um SqlDataReader
- [x] **ContraSqlInjection** - Realiza tratamento simples no SQL contra SQL Injection

###[Email](Funcoes/Classes/Email.cs)
- [x] **EnviarEmail** - Realiza o envio de e-mails

###[Json](Funcoes/Classes/Json.cs)
- [x] **DataTabelToJSON** - Transforma um DataTable em JSON

###[Xml](Funcoes/Classes/Xml.cs)
- [x] **StringToXmlDocument** - Transforma uma String em XmlDocument
- [x] **ObjectToXmlDocument** - Transforma um Object em XmlDocument
- [x] **StringToObject** - Transforma uma String XML em um Object
- [x] **XmlDocumentToString** - Transforma um XmlDocument em uma String
- [x] **ObjectToStringXML** - Transforma uma Object em uma String XML
- [x] **ValidarDocumentoXml** - Valida String XML conforme um XSD

###[Server](Funcoes/Classes/Server.cs)
- [x] **MontaSOAP** - Monta envelope SOAP
- [x] **UploadArquivoFTP** - Upload de arquivos para FTP

###[UtilDate](Funcoes/Classes/UtilDate.cs)
- [x] **DiferencaDias** - Retorna a diferença entre dias
- [x] **TruncarData** - Truca um DateTime
- [x] **DataExenso** - Retorna a data por extenso
- [x] **DiaSemana** - Retorna o dia da semana

###[UtilString](Funcoes/Classes/UtilString.cs)
- [x] **SubstringRight** - Realiza o Substring pela direita
- [x] **RemoveEspeciais** - Remove caracteres especiais
- [x] **RemoveAcentos** - Remove acentos
- [x] **RemoverCaracteresEspeciais** - Remove caracteres especiais
- [x] **SoNumeros** - Retorna somente numeros
- [x] **SoLetras** - Retorna somente letras
- [x] **AcertoDG** - Realiza o acerto DG
- [x] **ValorExtenso** - Retorna um valor por extenso

###[Validators](Funcoes/Classes/Validators.cs)
- [x] **ValidaEnderecoEmail** - Validação e-mail
- [x] **ValidaCnpj** - Validação CNPJ
- [x] **ValidaCpf** - Validação CPF
- [x] **ValidaPis** - Validação PIS

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
