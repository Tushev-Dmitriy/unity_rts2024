using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watchtower : Build
{
    public int radOfDetect;
    public float radOfBuild;
    public int countArchers = 5;
    public int minDist = 0;
    public int maxDist = 8;
    public int attackDelay = 2;
    public int damage = 5 * 5;
}
