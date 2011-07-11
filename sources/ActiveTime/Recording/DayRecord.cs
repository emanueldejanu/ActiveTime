﻿// ActiveTime
// Copyright (C) 2011 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;

namespace DustInTheWind.ActiveTime.Recording
{
    public class DayRecord
    {
        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private Record[] records;

        public Record[] Records
        {
            get { return records; }
            set { records = value; }
        }

        private string comment;

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        public bool IsEmpty
        {
            get { return (records == null || records.Length == 0) && (comment == null || comment.Length == 0); }
        }

        public bool HasRecords
        {
            get { return records != null && records.Length > 0; }
        }

        public bool HasComment
        {
            get { return comment != null && comment.Length > 0; }
        }

        public TimeSpan GetTotalTime()
        {
            TimeSpan totalTime = TimeSpan.Zero;

            if (records != null)
            {
                foreach (Record record in records)
                {
                    totalTime += record.EndTime - record.StartTime;
                }
            }

            return totalTime;
        }

        public TimeSpan GetIntervalTime()
        {
            TimeSpan totalTime = TimeSpan.Zero;

            if (records != null)
            {
                TimeSpan beginHour = records[0].StartTime;
                TimeSpan endHour = records[0].EndTime;

                foreach (Record record in records)
                {
                    if (record.StartTime < beginHour)
                        beginHour = record.StartTime;

                    if (record.EndTime > endHour)
                        endHour = record.EndTime;
                }

                totalTime = endHour - beginHour;
            }

            return totalTime;
        }

        public TimeSpan? GetBeginTime()
        {
            if (records != null && records.Length > 0)
            {
                return records[0].StartTime;
            }
            else
            {
                return null;
            }
        }

        //public DayTimeInterval CalculateLunchBreak(DayRecord dayRecord)
        //{
        //    if (dayRecord == null || dayRecord.Records == null || dayRecord.Records.Length < 2)
        //        return null; // no lunch break.

        //    TimeSpan breakStartHour = dayRecord.Records[0].EndTime;
        //    TimeSpan breakEndHour = dayRecord.Records[0].EndTime;

        //    List<DayTimeInterval> breaks = new List<DayTimeInterval>();

        //    foreach (Record record in dayRecord.Records)
        //    {
        //        breakEndHour = record.StartTime;
        //        breaks.Add(new DayTimeInterval(breakStartHour, breakEndHour));

        //        breakStartHour = record.EndTime;
        //    }
        //}

        public Record[] GetRecords(bool includeBreaks)
        {
            if (records == null || records.Length == 0 || !includeBreaks)
                return records;


            List<Record> allRecords = new List<Record>();

            allRecords.Add(records[0]);
            
            Record previousRecord = records[0];
            
            foreach (Record record in records)
            {
                allRecords.Add(new Break(previousRecord.Date, previousRecord.EndTime, record.StartTime));
                allRecords.Add(record);
                previousRecord = record;
            }

            return allRecords.ToArray();
        }
    }
}
