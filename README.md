# MetodosUteis
> DLL de métodos uteis em projetos .net

## Funções
### [Certificado](Funcoes/Funcoes/Classes/Certificado.cs)
<p>**AssinarXML** - Assinar XML</p>
<p>**AssinarXmlPorElemento** - Assinar XML por elemento</p>
<p>**X509CertificateToBase64** - Retornar certificado base 64</p>
<p>**EncryptMessage** - Criptografat mensagem com certificado</p>
<p>**DescryptMessage** - Descriptografar mensagem com certificado</p>
<p>**RetornarThumbprint** - Retornar certificado por thumbprint</p>
<p>**RetornarNome** - Retornar certificado por nome</p>
<p>**Vencido** - Verificar validade do certificado</p>
<p>**SelecionarPorNome** - Selecionar um certificado por nome</p>
<p>**SelecionarPorThumbprint** - Selecionar um certificado por thumbprint</p>
<p>**Base64ToX509Certificate** - Transformar certificado base 64 em X509Certificate2</p>

### [Criptografia](Funcoes/Funcoes/Classes/Criptografia.cs)
<p>**EncryptSHA** - Criptografar texto em SHA1</p>
<p>**EncryptASCHII** - Criptografar texto em ASCII</p>
<p>**DecryptASCHII** - Descriptografar texto em ASCII</p>
<p>**EncryptMD5** - Criptografar texto em MD5</p>
<p>**EncryptPass** - Criptografar texto utilizando palavra passe</p>
<p>**DecryptPass** - Descriptografar texto utilizando palavra passe</p>
<p>**EncryptAES** - Criptografar texto em AES</p>
<p>**DecryptAES** - Descriptografar texto em AES</p>

### [CriptografiaAes](Funcoes/Funcoes/Classes/CriptografiaAes.cs)
<p>Classe de criptografia AES</p>

### [CriptografiaCryptoJs](Funcoes/Funcoes/Classes/CriptografiaCryptoJs.cs)
<p>Classe de criptografia baseada no CryptoJs utilizando o formato AES sendo equivalenta a realizada no JavaScript</p>

### [BuscaCep](Funcoes/Funcoes/Classes/BuscaCep.cs)
<p>**ViaCEP** - Busca de CEP baseada nos serviços da ViaCEP (https://viacep.com.br)</p>

### [DataBase](Funcoes/Funcoes/Classes/DataBase.cs)
<p>**Sum** - Método que realiza o SUM de uma coluna</p>
<p>**Max** - Método que realiza o MAX de uma coluna</p>
<p>**Min** - Método que realiza o MIN de uma coluna</p>
<p>**Avg** - Método que realiza o AVG de uma coluna</p>
<p>**Count** - Método que realiza o COUNT de uma coluna</p>
<p>**Update** - Método que realiza o UPDATE de um registro</p>
<p>**Delete** - Método que realiza o DELETE de um registro</p>
<p>**Select** - Método que realiza o SELECT de um registro retornando um Object</p>
<p>**Select** - Método que realiza o SELECT de um registro retornando um DataTable</p>
<p>**Select** - Método que realiza o SELECT de um registro retornando um DataRowCollection</p>
<p>**ColunasTabela** - Método que realiza o SELECT de uma coluna da tabela retornando uma lista de valores</p>
<p>**CreateParametersFromString** - Método que criar os parametros e concactena os seus valores em suas respectivas condições</p>
<p>**ExisteParametro** - Método que verifica se o existe algum parametro já adicionado no arry de parametros</p>
<p>**MontaSQL** - Método que cria uma String SQL pra INSERT ou UPDATE</p>
<p>**PreencheObjetos** - Método responsável por preencher o objeto com um SqlDataReader</p>
<p>**PreencheObjetos** - Método responsável por preencher o objeto com um DataRow</p>
<p>**AddParametros** - Método responsável por criar os parametros de um sqlCommand</p>
<p>**ContraSqlInjection** - Método que trata ataques de SQL injection</p>

### [Email](Funcoes/Funcoes/Classes/Email.cs)
<p>**EnviarEmail** - Classe para realização de envio de e-mails</p>

### [Json](Funcoes/Funcoes/Classes/Json.cs)
<p>**DataTabelToJSON** - Converte um DataTable em uma String JSON</p>

### [Xml](Funcoes/Funcoes/Classes/Xml.cs)
<p>**StringToXmlDocument** - Converte uma STRING em um XmlDocument</p>
<p>**ObjectToXmlDocument** - Converte um OBJETO em um XmlDocument</p>
<p>**StringToObject** - Converte uma String XML em um Objeto</p>
<p>**XmlDocumentToString** - Converte um XmlDocument para uma String XML</p>
<p>**ObjectToStringXML** - Converte um Objeto para uma String XML</p>
<p>**ValidarDocumentoXml** - Realiza a validação de um XML conforme sua XSD</p>

### [Server](Funcoes/Funcoes/Classes/Server.cs)
<p>**MontaSOAP** - Método que realiza a montagem de um envelope SOAP</p>
<p>**UploadArquivoFTP** - Realiza o UPLOAD de arquivos para um servidor FT</p>

### [UtilDate](Funcoes/Funcoes/Classes/UtilDate.cs)
<p>**DivideLista** - Divide lista em listas menores</p>

### [UtilList](Funcoes/Funcoes/Classes/UtilList.cs)
<p>**DiferencaDias** - Retorna a diferença entre dias</p>

### [UtilString](Funcoes/Funcoes/Classes/UtilString.cs)
<p>**SubstringRight** - Realiza o Substring pela direita</p>
<p>**RemoveEspeciais** - Remove caracteres especiais</p>
<p>**RemoveAcentos** - Remove acentos</p>
<p>**RemoverCaracteresEspeciais** - Remove caracteres especiais</p>
<p>**SoNumeros** - Retorna somente os números do texto</p>
<p>**SoLetras** - Retorna somente as letras do texto</p>
<p>**AcertoDG** - Realiza o acerto DG</p>
<p>**ValorExtenso** - Retorna um valor por extenso</p>

### [Validators](Funcoes/Funcoes/Classes/Validators.cs)
<p>**ValidaEnderecoEmail** - Método que realiza a validação de um endereço de Email</p>
<p>**ValidaCnpj** - Método que realiza a validação de um CNPJ</p>
<p>**ValidaCpf** - Método que realiza a validação de um CPF</p>
<p>**ValidaPis** - Método que realiza a validação de um PIS</p>

### [DefaultValues - Extensões de valores default](Funcoes/Funcoes/Values/DefaultValues.cs)
### [ComplementValues - Extensões de complementos](Funcoes/Funcoes/Values/ComplementValues.cs)

## Utilização

```
Funcoes.[CLASSE].[METODO]
```

## Licença
Projeto desenvolvido para fins acadêmicos.
[MIT License](./LICENSE)
