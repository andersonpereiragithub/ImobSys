using ImobSys.Domain.Entities.Clientes;
using Newtonsoft.Json;
using System;

public class ClienteJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Cliente);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var jsonObject = Newtonsoft.Json.Linq.JObject.Load(reader);

        // Detecta se o JSON contém a propriedade "CPF" ou "CNPJ" para identificar o tipo
        if (jsonObject["CPF"] != null)
        {
            return jsonObject.ToObject<PessoaFisica>(serializer);
        }
        else if (jsonObject["CNPJ"] != null)
        {
            return jsonObject.ToObject<PessoaJuridica>(serializer);
        }

        throw new JsonSerializationException("Tipo de cliente desconhecido.");
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
}
