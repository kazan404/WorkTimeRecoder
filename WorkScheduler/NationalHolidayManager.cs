using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkScheduler
{
    static class NationalHolidayManager
    {
        // 国民の祝日
        // 1月1日　1月第二月曜
        // 2月11日
        // 3月春分節
        // 4月29日
        // 5月3日　5月4日　5月5日
        // 7月第三月曜
        // 8月11日
        // 9月15日　9月第三月曜　9月秋分節
        // 10月第二月曜
        // 11月3日 11月23日
        // 12月23日

        // 日が固定の休日。"年"部分はDateTime型作成のためのダミー
        static private DateTime[] fixatHoridayArray = {new DateTime(2017, 1,1),
                                                       new DateTime(2017, 2,11),
                                                       new DateTime(2017, 4,29),
                                                       new DateTime(2017, 5,3),
                                                       new DateTime(2017, 5,4),
                                                       new DateTime(2017, 5,5),
                                                       new DateTime(2017, 8,11),
                                                       new DateTime(2017, 9,15),
                                                       new DateTime(2017, 11,3),
                                                       new DateTime(2017, 11,23),
                                                       new DateTime(2017, 12,23),
                                                      };

        // 日が固定ではない休日リスト
        static private List<DateTime> unFixatHoridayList = new List<DateTime>{new DateTime(2017, 9, 23),
                                                                       new DateTime(2018, 9, 23),};

        static bool IsNationalHoriday(DateTime date)
        {

            return false;
        }

        // 日付固定の祝日
        static private bool IsFixationHoriday(DateTime date)
        {
            bool flg_horiday = false;

            // 毎年日付が固定のため、「年」を考えずに、月と日の一致で判定する。
            for (int i = 0; i < fixatHoridayArray.Length; i++ )
            {
                if(date.Month == fixatHoridayArray[i].Month &&
                    date.Day == fixatHoridayArray[i].Day)
                {
                    flg_horiday = true;
                    break;
                }
            }

            return flg_horiday;
        }

        static private bool IsFrexibleHoriday(DateTime date)
        {
            // 第～月曜
            // 1月第二月曜、7月第三月曜、9月第三月曜、10月第二月曜
            if(date.DayOfWeek == DayOfWeek.Monday)
            {

            }

            // 時候
            // 3月春分節　9月秋分節

            return false;
        }

        static private int CountWeekTimeInMonth(DateTime date, int month, DayOfWeek week)
        {
            int count = 0;
            for (int i = 1; i < date.Day + 1; i++ )
            {
                DateTime chkerDateTime = new DateTime(date.Year, date.Month, i);
                if(chkerDateTime.DayOfWeek == week)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
