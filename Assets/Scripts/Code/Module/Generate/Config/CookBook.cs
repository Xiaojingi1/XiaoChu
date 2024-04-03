using System;
using System.Collections.Generic;
using Nino.Serialization;

namespace TaoTie
{
    [NinoSerialize]
    [Config]
    public partial class CookBookCategory : ProtoObject, IMerge
    {
        public static CookBookCategory Instance;
		
        
        [NinoIgnore]
        private Dictionary<int, CookBook> dict = new Dictionary<int, CookBook>();
        
        [NinoMember(1)]
        private List<CookBook> list = new List<CookBook>();
		
        public CookBookCategory()
        {
            Instance = this;
        }
        
        public void Merge(object o)
        {
            CookBookCategory s = o as CookBookCategory;
            this.list.AddRange(s.list);
        }
		
        public override void EndInit()
        {
            for(int i =0 ;i<list.Count;i++)
            {
                CookBook config = list[i];
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public CookBook Get(int id)
        {
            this.dict.TryGetValue(id, out CookBook item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (CookBook)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, CookBook> GetAll()
        {
            return this.dict;
        }
        public List<CookBook> GetAllList()
        {
            return this.list;
        }
        public CookBook GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [NinoSerialize]
	public partial class CookBook: ProtoObject
	{
		/// <summary>Id</summary>
		[NinoMember(1)]
		public int Id { get; set; }
		/// <summary>菜名</summary>
		[NinoMember(2)]
		public string name { get; set; }
		/// <summary>配方</summary>
		[NinoMember(3)]
		public int[] Batching { get; set; }

	}
}
