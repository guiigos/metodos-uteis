# MetodosUteis
> DLL de métodos uteis em projetos .net

## Funções
### [Certificado](Funcoes/Funcoes/Classes/Certificado.cs)
> <p><strong>AssinarXML</strong> - Assinar XML</p>
> <p><strong>AssinarXmlPorElemento</strong> - Assinar XML por elemento</p>
> <p><strong>X509CertificateToBase64</strong> - Retornar certificado base 64</p>
> <p><strong>EncryptMessage</strong> - Criptografat mensagem com certificado</p>
> <p><strong>DescryptMessage</strong> - Descriptografar mensagem com certificado</p>
> <p><strong>RetornarThumbprint</strong> - Retornar certificado por thumbprint</p>
> <p><strong>RetornarNome</strong> - Retornar certificado por nome</p>
> <p><strong>Vencido</strong> - Verificar validade do certificado</p>
> <p><strong>SelecionarPorNome</strong> - Selecionar um certificado por nome</p>
> <p><strong>SelecionarPorThumbprint</strong> - Selecionar um certificado por thumbprint</p>

> **Base64ToX509Certificate** - Transformar certificado base 64 em X509Certificate2

### [Criptografia](Funcoes/Funcoes/Classes/Criptografia.cs)
> <p><strong>EncryptSHA</strong> - Criptografar texto em SHA1</p>
> <p><strong>EncryptASCHII</strong> - Criptografar texto em ASCII</p>
> <p><strong>DecryptASCHII</strong> - Descriptografar texto em ASCII</p>
> <p><strong>EncryptMD5</strong> - Criptografar texto em MD5</p>
> <p><strong>EncryptPass</strong> - Criptografar texto utilizando palavra passe</p>
> <p><strong>DecryptPass</strong> - Descriptografar texto utilizando palavra passe</p>
> <p><strong>EncryptAES</strong> - Criptografar texto em AES</p>
> <p><strong>DecryptAES</strong> - Descriptografar texto em AES</p>

### [CriptografiaAes](Funcoes/Funcoes/Classes/CriptografiaAes.cs)
> Classe de criptografia AES

### [CriptografiaCryptoJs](Funcoes/Funcoes/Classes/CriptografiaCryptoJs.cs)
> Classe de criptografia baseada no CryptoJs utilizando o formato AES sendo equivalenta a realizada no JavaScript

### [BuscaCep](Funcoes/Funcoes/Classes/BuscaCep.cs)
> **ViaCEP** - Busca de CEP baseada nos serviços da ViaCEP (https://viacep.com.br)

### [DataBase](Funcoes/Funcoes/Classes/DataBase.cs)
> <p><strong>Sum</strong> - Método que realiza o SUM de uma coluna</p>
> <p><strong>Max</strong> - Método que realiza o MAX de uma coluna</p>
> <p><strong>Min</strong> - Método que realiza o MIN de uma coluna</p>
> <p><strong>Avg</strong> - Método que realiza o AVG de uma coluna</p>
> <p><strong>Count</strong> - Método que realiza o COUNT de uma coluna</p>
> <p><strong>Update</strong> - Método que realiza o UPDATE de um registro</p>
> <p><strong>Delete</strong> - Método que realiza o DELETE de um registro</p>
> <p><strong>Select</strong> - Método que realiza o SELECT de um registro retornando um Object</p>
> <p><strong>Select</strong> - Método que realiza o SELECT de um registro retornando um DataTable</p>
> <p><strong>Select</strong> - Método que realiza o SELECT de um registro retornando um DataRowCollection</p>
> <p><strong>ColunasTabela</strong> - Método que realiza o SELECT de uma coluna da tabela retornando uma lista de valores</p>
> <p><strong>CreateParametersFromString</strong> - Método que criar os parametros e concactena os seus valores em suas respectivas condições</p>
> <p><strong>ExisteParametro</strong> - Método que verifica se o existe algum parametro já adicionado no arry de parametros</p>
> <p><strong>MontaSQL</strong> - Método que cria uma String SQL pra INSERT ou UPDATE</p>
> <p><strong>PreencheObjetos</strong> - Método responsável por preencher o objeto com um SqlDataReader</p>
> <p><strong>PreencheObjetos</strong> - Método responsável por preencher o objeto com um DataRow</p>
> <p><strong>AddParametros</strong> - Método responsável por criar os parametros de um sqlCommand</p>
> <p><strong>ContraSqlInjection</strong> - Método que trata ataques de SQL injection</p>

### [Email](Funcoes/Funcoes/Classes/Email.cs)
> **EnviarEmail** - Classe para realização de envio de e-mails

### [Json](Funcoes/Funcoes/Classes/Json.cs)
> **DataTabelToJSON** - Converte um DataTable em uma String JSON

### [Xml](Funcoes/Funcoes/Classes/Xml.cs)
> <p><strong>StringToXmlDocument</strong> - Converte uma STRING em um XmlDocument</p>
> <p><strong>ObjectToXmlDocument</strong> - Converte um OBJETO em um XmlDocument</p>
> <p><strong>StringToObject</strong> - Converte uma String XML em um Objeto</p>
> <p><strong>XmlDocumentToString</strong> - Converte um XmlDocument para uma String XML</p>
> <p><strong>ObjectToStringXML</strong> - Converte um Objeto para uma String XML</p>
> <p><strong>ValidarDocumentoXml</strong> - Realiza a validação de um XML conforme sua XSD</p>

### [Server](Funcoes/Funcoes/Classes/Server.cs)
> <p><strong>MontaSOAP</strong> - Método que realiza a montagem de um envelope SOAP</p>
> <p><strong>UploadArquivoFTP</strong> - Realiza o UPLOAD de arquivos para um servidor FT</p>

### [UtilDate](Funcoes/Funcoes/Classes/UtilDate.cs)
> <p><strong>DivideLista</strong> - Divide lista em listas menores</p>

### [UtilList](Funcoes/Funcoes/Classes/UtilList.cs)
> <p><strong>DiferencaDias</strong> - Retorna a diferença entre dias</p>

### [UtilString](Funcoes/Funcoes/Classes/UtilString.cs)
> <p><strong>SubstringRight</strong> - Realiza o Substring pela direita</p>
> <p><strong>RemoveEspeciais</strong> - Remove caracteres especiais</p>
> <p><strong>RemoveAcentos</strong> - Remove acentos</p>
> <p><strong>RemoverCaracteresEspeciais</strong> - Remove caracteres especiais</p>
> <p><strong>SoNumeros</strong> - Retorna somente os números do texto</p>
> <p><strong>SoLetras</strong> - Retorna somente as letras do texto</p>
> <p><strong>AcertoDG</strong> - Realiza o acerto DG</p>
> <p><strong>ValorExtenso</strong> - Retorna um valor por extenso</p>

### [Validators](Funcoes/Funcoes/Classes/Validators.cs)
> <p><strong>ValidaEnderecoEmail</strong> - Método que realiza a validação de um endereço de Email</p>
> <p><strong>ValidaCnpj</strong> - Método que realiza a validação de um CNPJ</p>
> <p><strong>ValidaCpf</strong> - Método que realiza a validação de um CPF</p>
> <p><strong>ValidaPis</strong> - Método que realiza a validação de um PIS</p>

### [DefaultValues - Extensões de valores default](Funcoes/Funcoes/Values/DefaultValues.cs)
### [ComplementValues - Extensões de complementos](Funcoes/Funcoes/Values/ComplementValues.cs)

## Utilização

```
Funcoes.[CLASSE].[METODO]
```

## Licença
Projeto desenvolvido para fins acadêmicos.
[MIT License](./LICENSE)
