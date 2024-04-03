/* this is generated by nino */
namespace TaoTie
{
    public partial class Ingredient
    {
        public static Ingredient.SerializationHelper NinoSerializationHelper = new Ingredient.SerializationHelper();
        public class SerializationHelper: Nino.Serialization.NinoWrapperBase<Ingredient>
        {
            #region NINO_CODEGEN
            public override void Serialize(Ingredient value, Nino.Serialization.Writer writer)
            {
                if(value == null)
                {
                    writer.Write(false);
                    return;
                }
                writer.Write(true);
                writer.CompressAndWrite(value.Id);
                writer.Write(value.IconName);
            }

            public override Ingredient Deserialize(Nino.Serialization.Reader reader)
            {
                if(!reader.ReadBool())
                    return null;
                Ingredient value = new Ingredient();
                value.Id = reader.DecompressAndReadNumber<System.Int32>();
                value.IconName = reader.ReadString();
                return value;
            }
            #endregion
        }
    }
}