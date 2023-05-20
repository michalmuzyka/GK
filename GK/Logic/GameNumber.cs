﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK.Logic;

public partial class GameNumber : ObservableObject
{
    [ObservableProperty()]
    public int number;

    [ObservableProperty()]
    public Player? player;

    [ObservableProperty()]
    public bool inWinningSequence;

    public bool Clickable => Player == null;
}