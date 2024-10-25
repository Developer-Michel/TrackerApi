using System;
using System.Collections.Generic;

namespace Tracker_Api.Models.Tracker;

public partial class TrackingData
{
    public string User { get; set; } = null!;

    public DateOnly Date { get; set; }

    public short SleepTime { get; set; }

    public short SleepQuality { get; set; }

    public short MeditationTime { get; set; }

    public short ExerciseTime { get; set; }

    public short HapinessLevel { get; set; }

    public short StressLevel { get; set; }

    public short AnxietyLevel { get; set; }

    public string HappySentence { get; set; } = null!;
}
