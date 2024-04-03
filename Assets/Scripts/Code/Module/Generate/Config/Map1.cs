using System;
using System.Collections.Generic;
using Nino.Serialization;

namespace TaoTie
{
    [NinoSerialize]
    [Config]
    public partial class Map1Category : ProtoObject, IMerge
    {
        public static Map1Category Instance;
		
        
        [NinoIgnore]
        private Dictionary<int, Map1> dict = new Dictionary<int, Map1>();
        
        [NinoMember(1)]
        private List<Map1> list = new List<Map1>();
		
        public Map1Category()
        {
            Instance = this;
        }
        
        public void Merge(object o)
        {
            Map1Category s = o as Map1Category;
            this.list.AddRange(s.list);
        }
		
        public override void EndInit()
        {
            for(int i =0 ;i<list.Count;i++)
            {
                Map1 config = list[i];
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public Map1 Get(int id)
        {
            this.dict.TryGetValue(id, out Map1 item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (Map1)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, Map1> GetAll()
        {
            return this.dict;
        }
        public List<Map1> GetAllList()
        {
            return this.list;
        }
        public Map1 GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [NinoSerialize]
	public partial class Map1: ProtoObject
	{
		/// <summary>Id</summary>
		[NinoMember(1)]
		public int Id { get; set; }
		/// <summary>配方</summary>
		[NinoMember(2)]
		public int[] Batching { get; set; }

	}
}
