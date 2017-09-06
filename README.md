# MetodosUteis
> DLL de métodos uteis em projetos .net

## Funções
### [Certificado](Funcoes/Funcoes/Classes/Certificado.cs)
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

### [Criptografia](Funcoes/Funcoes/Classes/Criptografia.cs)
- **EncryptSHA** - Criptografar texto em SHA1
- **EncryptASCHII** - Criptografar texto em ASCII
- **DecryptASCHII** - Descriptografar texto em ASCII
- **EncryptMD5** - Criptografar texto em MD5
- **EncryptPass** - Criptografar texto utilizando palavra passe
- **DecryptPass** - Descriptografar texto utilizando palavra passe
- **EncryptAES** - Criptografar texto em AES
- **DecryptAES** - Descriptografar texto em AES

### [CriptografiaAes](Funcoes/Funcoes/Classes/CriptografiaAes.cs)
- Classe de criptografia AES

### [CriptografiaCryptoJs](Funcoes/Funcoes/Classes/CriptografiaCryptoJs.cs)
- Classe de criptografia baseada no CryptoJs utilizando o formato AES sendo equivalenta a realizada no JavaScript

### [BuscaCep](Funcoes/Funcoes/Classes/BuscaCep.cs)
- **ViaCEP** - Busca de CEP baseada nos serviços da ViaCEP (https://viacep.com.br)

### [DataBase](Funcoes/Funcoes/Classes/DataBase.cs)
- **Sum** - Método que realiza o SUM de uma coluna
- **Max** - Método que realiza o MAX de uma coluna
- **Min** - Método que realiza o MIN de uma coluna
- **Avg** - Método que realiza o AVG de uma coluna
- **Count** - Método que realiza o COUNT de uma coluna
- **Update** - Método que realiza o UPDATE de um registro
- **Delete** - Método que realiza o DELETE de um registro
- **Select** - Método que realiza o SELECT de um registro retornando um Object
- **Select** - Método que realiza o SELECT de um registro retornando um DataTable
- **Select** - Método que realiza o SELECT de um registro retornando um DataRowCollection
- **ColunasTabela** - Método que realiza o SELECT de uma coluna da tabela retornando uma lista de valores
- **CreateParametersFromString** - Método que criar os parametros e concactena os seus valores em suas respectivas condições
- **ExisteParametro** - Método que verifica se o existe algum parametro já adicionado no arry de parametros
- **MontaSQL** - Método que cria uma String SQL pra INSERT ou UPDATE
- **PreencheObjetos** - Método responsável por preencher o objeto com um SqlDataReader
- **PreencheObjetos** - Método responsável por preencher o objeto com um DataRow
- **AddParametros** - Método responsável por criar os parametros de um sqlCommand
- **ContraSqlInjection** - Método que trata ataques de SQL injection

### [Email](Funcoes/Funcoes/Classes/Email.cs)
- **EnviarEmail** - Classe para realização de envio de e-mails

### [Json](Funcoes/Funcoes/Classes/Json.cs)
- **DataTabelToJSON** - Converte um DataTable em uma String JSON

### [Xml](Funcoes/Funcoes/Classes/Xml.cs)
- **StringToXmlDocument** - Converte uma STRING em um XmlDocument
- **ObjectToXmlDocument** - Converte um OBJETO em um XmlDocument
- **StringToObject** - Converte uma String XML em um Objeto
- **XmlDocumentToString** - Converte um XmlDocument para uma String XML
- **ObjectToStringXML** - Converte um Objeto para uma String XML
- **ValidarDocumentoXml** - Realiza a validação de um XML conforme sua XSD

### [Server](Funcoes/Funcoes/Classes/Server.cs)
- **MontaSOAP** - Método que realiza a montagem de um envelope SOAP
- **UploadArquivoFTP** - Realiza o UPLOAD de arquivos para um servidor FT

### [UtilDate](Funcoes/Funcoes/Classes/UtilDate.cs)
- **DivideLista** - Divide lista em listas menores

### [UtilList](Funcoes/Funcoes/Classes/UtilList.cs)
- **DiferencaDias** - Retorna a diferença entre dias

### [UtilString](Funcoes/Funcoes/Classes/UtilString.cs)
- **SubstringRight** - Realiza o Substring pela direita
- **RemoveEspeciais** - Remove caracteres especiais
- **RemoveAcentos** - Remove acentos
- **RemoverCaracteresEspeciais** - Remove caracteres especiais
- **SoNumeros** - Retorna somente os números do texto
- **SoLetras** - Retorna somente as letras do texto
- **AcertoDG** - Realiza o acerto DG
- **ValorExtenso** - Retorna um valor por extenso

### [Validators](Funcoes/Funcoes/Classes/Validators.cs)
- **ValidaEnderecoEmail** - Método que realiza a validação de um endereço de Email
- **ValidaCnpj** - Método que realiza a validação de um CNPJ
- **ValidaCpf** - Método que realiza a validação de um CPF
- **ValidaPis** - Método que realiza a validação de um PIS

### [DefaultValues - Extensões de valores default](Funcoes/Funcoes/Values/DefaultValues.cs)
### [ComplementValues - Extensões de complementos](Funcoes/Funcoes/Values/ComplementValues.cs)

## Utilização

```
Funcoes.[CLASSE].[METODO]
```

## Licença
Projeto desenvolvido para fins acadêmicos.
[MIT License](./LICENSE)
