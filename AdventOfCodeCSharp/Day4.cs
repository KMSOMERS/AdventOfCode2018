using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeCSharp
{
    public class Day4
    {

        public enum Action
        {
            WOKE_UP,
            FALLS_ASLEEP,
            START_SHIFT
        }

        private class GuardEvent
        {
            public DateTime Date { get; set; }
            public Action Action { get; set; }
            public string GuardId { get; set; }
            public string Context { get; set; }

            public GuardEvent(string date)
            {
                Date = DateTime.Parse(date);
            }
        }

        private class Guard
        {
            public string Id { get; set; }
            public List<GuardEvent> Events { get; set; }

            public double AwakeTime { get; set; }
            public double AsleepTime { get; set; }

            public Dictionary<int, int> SleepingTally = new Dictionary<int, int>
            {
                { 0, 0 },  { 21, 0 }, { 42, 0 },
                { 1, 0 },  { 22, 0 }, { 43, 0 },
                { 2, 0 },  { 23, 0 }, { 44, 0 },
                { 3, 0 },  { 24, 0 }, { 45, 0 },
                { 4, 0 },  { 25, 0 }, { 46, 0 },
                { 5, 0 },  { 26, 0 }, { 47, 0 },
                { 6, 0 },  { 27, 0 }, { 48, 0 },
                { 7, 0 },  { 28, 0 }, { 49, 0 },
                { 8, 0 },  { 29, 0 }, { 50, 0 },
                { 9, 0 },  { 30, 0 }, { 51, 0 },
                { 10, 0 }, { 31, 0 }, { 52, 0 },
                { 11, 0 }, { 32, 0 }, { 53, 0 },
                { 12, 0 }, { 33, 0 }, { 54, 0 },
                { 13, 0 }, { 34, 0 }, { 55, 0 },
                { 14, 0 }, { 35, 0 }, { 56, 0 },
                { 15, 0 }, { 36, 0 }, { 57, 0 },
                { 16, 0 }, { 37, 0 }, { 58, 0 },
                { 17, 0 }, { 38, 0 }, { 59, 0 },
                { 18, 0 }, { 39, 0 },
                { 19, 0 }, { 40, 0 },
                { 20, 0 }, { 41, 0 }
            };

            public Guard(string id)
            {
                Events = new List<GuardEvent>();
                Id = id;
            }

            public void AddEvent(GuardEvent guardEvent)
            {
                Events.Add(guardEvent);
            }
        }

        // Example line
        // [1518-03-28 00:04] Guard #2663 begins shift
        public static void ExerciseOne()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Projects\AdventOfCode\AdventOfCode2018\AdventOfCodeCSharp\Day4.txt");

            List<GuardEvent> events = new List<GuardEvent>();
            Dictionary<string, Guard> guards = new Dictionary<string, Guard>();

            foreach (string line in lines)
            {
                string date = line.Substring(1, 16);
                GuardEvent guardEvent = new GuardEvent(date);

                string action = line.Substring(19);

                if (action.Contains("falls asleep"))
                {
                    guardEvent.Action = Action.FALLS_ASLEEP;
                }
                else if (action.Contains("wakes up"))
                {
                    guardEvent.Action = Action.WOKE_UP;
                }
                else
                {
                    guardEvent.Action = Action.START_SHIFT;
                }

                guardEvent.Context = action;

                events.Add(guardEvent);
            }

            events.Sort((a,b) => a.Date.CompareTo(b.Date));

            string currentGuardId = string.Empty;
            foreach (GuardEvent guardEvent in events)
            {
                string[] boop = guardEvent.Context.Split(" ");

                if (guardEvent.Action == Action.START_SHIFT)
                {
                    currentGuardId = boop[1].Substring(1);
                    if (!guards.ContainsKey(currentGuardId))
                    {
                        Guard guard = new Guard(currentGuardId);
                        guards.Add(currentGuardId, guard);
                    }
                }

                Guard curentGuard = guards[currentGuardId];
                curentGuard.AddEvent(guardEvent);
                guardEvent.GuardId = currentGuardId;
            }

            foreach (string key in guards.Keys)
            {
                Guard guard = guards[key];

                GuardEvent sleepEvent = guard.Events[0];
                foreach (GuardEvent guardEvent in guard.Events)
                {
                    if (guardEvent.Action == Action.FALLS_ASLEEP)
                    {
                        sleepEvent = guardEvent;
                    }
                    else if (guardEvent.Action == Action.WOKE_UP)
                    {
                        double sleepMinutes = (guardEvent.Date.Subtract(sleepEvent.Date).TotalMinutes) - 1;
                        guard.AsleepTime += sleepMinutes + 1;

                        int startMinute = sleepEvent.Date.Minute;
                        for (double i = sleepMinutes; i >= 0; i--)
                        {

                                guard.SleepingTally[startMinute]++;
                                startMinute++;
                        }
                    }
                }
            }

            Guard longestSleepGuard = new Guard("INVALID");
            foreach (string key in guards.Keys)
            {
                if (guards[key].AsleepTime > longestSleepGuard.AsleepTime)
                {
                    longestSleepGuard = guards[key];
                }
            }

            int longestMinuteSleptKey = 0;
            int longestSlept = 0;
            foreach (int key in longestSleepGuard.SleepingTally.Keys)
            {
                if (longestSleepGuard.SleepingTally[key] > longestSlept)
                {
                    longestMinuteSleptKey = key;
                    longestSlept = longestSleepGuard.SleepingTally[key];
                }
            }

            Console.WriteLine(Convert.ToInt32(longestSleepGuard.Id) * longestMinuteSleptKey);
            Console.ReadKey();
        }

        public static void ExerciseTwo()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Projects\AdventOfCode\AdventOfCode2018\AdventOfCodeCSharp\Day4.txt");

            List<GuardEvent> events = new List<GuardEvent>();
            Dictionary<string, Guard> guards = new Dictionary<string, Guard>();

            foreach (string line in lines)
            {
                string date = line.Substring(1, 16);
                GuardEvent guardEvent = new GuardEvent(date);

                string action = line.Substring(19);

                if (action.Contains("falls asleep"))
                {
                    guardEvent.Action = Action.FALLS_ASLEEP;
                }
                else if (action.Contains("wakes up"))
                {
                    guardEvent.Action = Action.WOKE_UP;
                }
                else
                {
                    guardEvent.Action = Action.START_SHIFT;
                }

                guardEvent.Context = action;

                events.Add(guardEvent);
            }

            events.Sort((a, b) => a.Date.CompareTo(b.Date));

            string currentGuardId = string.Empty;
            foreach (GuardEvent guardEvent in events)
            {
                string[] boop = guardEvent.Context.Split(" ");

                if (guardEvent.Action == Action.START_SHIFT)
                {
                    currentGuardId = boop[1].Substring(1);
                    if (!guards.ContainsKey(currentGuardId))
                    {
                        Guard guard = new Guard(currentGuardId);
                        guards.Add(currentGuardId, guard);
                    }
                }

                Guard curentGuard = guards[currentGuardId];
                curentGuard.AddEvent(guardEvent);
                guardEvent.GuardId = currentGuardId;
            }

            foreach (string key in guards.Keys)
            {
                Guard guard = guards[key];

                GuardEvent sleepEvent = guard.Events[0];
                foreach (GuardEvent guardEvent in guard.Events)
                {
                    if (guardEvent.Action == Action.FALLS_ASLEEP)
                    {
                        sleepEvent = guardEvent;
                    }
                    else if (guardEvent.Action == Action.WOKE_UP)
                    {
                        double sleepMinutes = (guardEvent.Date.Subtract(sleepEvent.Date).TotalMinutes) - 1;
                        guard.AsleepTime += sleepMinutes + 1;

                        int startMinute = sleepEvent.Date.Minute;
                        for (double i = sleepMinutes; i >= 0; i--)
                        {

                            guard.SleepingTally[startMinute]++;
                            startMinute++;
                        }
                    }
                }
            }

            string longestMinuteSleptGuardKey = string.Empty;
            int longestMinuteSleptKey = 0;
            int currentLongestSlept = 0;
            foreach (string key in guards.Keys)
            {
                foreach (int tallyKey in guards[key].SleepingTally.Keys)
                {
                    if (guards[key].SleepingTally[tallyKey] > currentLongestSlept)
                    {
                        longestMinuteSleptGuardKey = key;
                        longestMinuteSleptKey = tallyKey;
                        currentLongestSlept = guards[key].SleepingTally[tallyKey];
                    }
                }
            }

            Console.WriteLine(Convert.ToInt32(longestMinuteSleptGuardKey) * longestMinuteSleptKey);
            Console.ReadKey();
        }
    }
}
