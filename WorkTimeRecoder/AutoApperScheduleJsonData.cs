using Newtonsoft.Json;

namespace WorkTimeRecoder
{
    [JsonObject("AutoApperSchedule")]
    class AutoApperScheduleJsonData
    {
        [JsonProperty("Schedule Name")]
        public string Name { get; set; }

        [JsonProperty("Schedule IDNumber")]
        public string IDNumber { get; set; }

        private AutoApperScheduleJsonData()
        {

        }

        public AutoApperScheduleJsonData(string scheduleName, string scheduleIDNumber)
        {
            Name = scheduleName;
            IDNumber = scheduleIDNumber;
        }
    }
}
