using System;

public class Flop
{
    private int counter = 0;

    public void Every(int flop, Action callback)
    {
        counter++;
        if (counter >= flop)
        {
            callback.Invoke();
            counter = 0;
        }
    }
}