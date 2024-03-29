using System;
using System.Collections.Generic;
using Nino.Serialization;

namespace TaoTie
{
    [NinoSerialize]
    [Config]
    public partial class LevelCategory : ProtoObject, IMerge
    {
        public static LevelCategory Instance;
		
        
        [NinoIgnore]
        private Dictionary<int, Level> dict = new Dictionary<int, Level>();
        
        [NinoMember(1)]
        private List<Level> list = new List<Level>();
		
        public LevelCategory()
        {
            Instance = this;
        }
        
        public void Merge(object o)
        {
            LevelCategory s = o as LevelCategory;
            this.list.AddRange(s.list);
        }
		
        public override void EndInit()
        {
            for(int i =0 ;i<list.Count;i++)
            {
                Level config = list[i];
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public Level Get(int id)
        {
            this.dict.TryGetValue(id, out Level item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (Level)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, Level> GetAll()
        {
            return this.dict;
        }
        public List<Level> GetAllList()
        {
            return this.list;
        }
        public Level GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [NinoSerialize]
	public partial class Level: ProtoObject
	{
		/// <summary>Id</summary>
		[NinoMember(1)]
		public int Id { get; set; }
		/// <summary>层数</summary>
		[NinoMember(2)]
		public int Layer { get; set; }

	}
}
