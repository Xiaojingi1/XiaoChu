using System;
using System.Collections.Generic;
using Nino.Serialization;

namespace TaoTie
{
    [NinoSerialize]
    [Config]
    public partial class IngredientCategory : ProtoObject, IMerge
    {
        public static IngredientCategory Instance;
		
        
        [NinoIgnore]
        private Dictionary<int, Ingredient> dict = new Dictionary<int, Ingredient>();
        
        [NinoMember(1)]
        private List<Ingredient> list = new List<Ingredient>();
		
        public IngredientCategory()
        {
            Instance = this;
        }
        
        public void Merge(object o)
        {
            IngredientCategory s = o as IngredientCategory;
            this.list.AddRange(s.list);
        }
		
        public override void EndInit()
        {
            for(int i =0 ;i<list.Count;i++)
            {
                Ingredient config = list[i];
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public Ingredient Get(int id)
        {
            this.dict.TryGetValue(id, out Ingredient item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (Ingredient)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, Ingredient> GetAll()
        {
            return this.dict;
        }
        public List<Ingredient> GetAllList()
        {
            return this.list;
        }
        public Ingredient GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [NinoSerialize]
	public partial class Ingredient: ProtoObject
	{
		/// <summary>Id</summary>
		[NinoMember(1)]
		public int Id { get; set; }
		/// <summary>名称</summary>
		[NinoMember(2)]
		public string IconName { get; set; }

	}
}
