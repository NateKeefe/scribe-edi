# Scribe Labs - EDI
A framework for building Scribe connectors using [EDI.NET](https://github.com/indice-co/EDI.Net) and [Scribe Labs Reflector](https://github.com/NateKeefe/Reflector).

# Quick Start
[EDI.NET](https://github.com/indice-co/EDI.Net) is a .NET EDI Serializer/Deserializer that supports EDIFact, X12 and TRADACOMS formats.

[Scribe Labs Reflector](https://github.com/NateKeefe/Reflector) allows for attributing .NET classes and properties to generate Scribe metadata and translate to data entities.

These components together allow you to generate a single .NET class, attribute with EDI.NET and Reflector, and De/Serialize between EDI formats and Scribe Data Entities.

## Class definition
Define your .NET class and attribute with coorresponding Reflector and EDI.NET attributes. 

```csharp
using System;
using System.Collections.Generic;
using indice.Edi.Serialization;
using Scribe.Connector.Common.Reflection;
using Scribe.Connector.Common.Reflection.Actions;

namespace x12.models.810
{
    [ObjectDefinition(Hidden = false, Name = "Invoices")] //Scribe Object Metadata (renamed)
    [CreateWith] //Scribe Action Metadata
    [Query] //Scribe Action Metadata
    public class Invoice_810 //.NET type
    {
        [PropertyDefinition(Description = "ISA01 - Authorization Information Qualifier")] //Scribe Property Metadata
        [EdiValue("9(2)", Path = "ISA/0", Description = "ISA01 - Authorization Information Qualifier")] //EDI.NET annotations
        public int AuthorizationInformationQualifier { get; set; } //.NET type
        
        ....
    }
````
See more information at [EDI.NET attributes](https://github.com/indice-co/EDI.Net#attributes).

## Derialization
You can create a method that will return Scribe data entities based on the EDI body using its serializer and Scribe Reflector. 

```csharp
public static IEnumerable<DataEntity> ReadEDI<T>(string raw, Query query, Reflector r)
{
    var grammar = EdiGrammar.NewX12(); //choose EDI format type
    var n = default(T); //instantiate your class
    using (TextReader sr = new StringReader(raw)) 
      { n = new EdiSerializer().Deserialize<T>(sr, grammar); } //use EDI.NET serializer; string->.NET
    return r.ToDataEntities(new[] { n }, query.RootEntity); //use Reflector; .NET->DataEntity
}
````

## Serialization
Create a method that will return a populated .NET class from Scribe input data entities.

```csharp
private T ToScribeModel<T>(DataEntity input) where T : new()
{
    T scribeModel;
    try
    {
        scribeModel = reflector.To<T>(input); //use Reflector; DataEntity->.NET
    }
    catch (Exception e)
    {
        throw new ArgumentException("Error translating from DataEntity to ScribeModel: " + e.Message, e);
    }
    return scribeModel;
}
````
Create a method to serialize your .NET classes into EDI data (and return a data entity). 

```csharp
public OperationResult Create(DataEntity dataEntity)
  {
    var entityName = dataEntity.ObjectDefinitionFullName;
    var operationResult = new OperationResult();
    var output = new DataEntity(entityName);

    switch (entityName)
      {
        case "Invoices":
        var invoice810 = ToScribeModel<Invoice_810>(dataEntity); //Reflector Method; DataEntity->.NET
        var grammar = EdiGrammar.NewX12();
        using (var textWriter = new StreamWriter(File.Open(@"c:\temp\invoice.edi", FileMode.Create))) //POC
        {
            using (var ediWriter = new EdiTextWriter(textWriter, grammar))
            {
                new EdiSerializer().Serialize(ediWriter, invoice810); //EDI.NET classes to text
            }
        }
      break;
    
      default:
        throw new ArgumentException($"{entityName} is not supported for Create.");
      }
        operationResult = new OperationResult
          {
            ErrorInfo = new ErrorResult[] { null },
            ObjectsAffected = new[] { 1 },
            Output = new[] { output },
            Success = new[] { true }
           };
        return operationResult;
    }
````
