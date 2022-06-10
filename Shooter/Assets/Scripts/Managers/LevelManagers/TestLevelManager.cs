using UnityEngine;

public class TestLevelManager : LevelManager
{
    [field: SerializeField] private GameObject Boss { get; set; }
    
    [field: SerializeField] private GameObject Projectile1 { get; set; }
    [field: SerializeField] private GameObject Projectile2 { get; set; }
    [field: SerializeField] private GameObject Projectile3 { get; set; }
    
    [field: SerializeField] private GameObject Soldier { get; set; }
    [field: SerializeField] private GameObject SergeantSingle { get; set; }
    [field: SerializeField] private GameObject SergeantDouble { get; set; }

    
    private const string HDangle = "HDangle";
    private const string HSlide1 = "HSlide1";
    private const string HSlide2 = "HSlide2";
    private const string Test = "Test";
    private const string VDangle = "VDangle";
    private const string VSlide1 = "VSlide1";
    private const string VSlide2 = "VSlide2";
    private const string Wave = "Wave";
    private const string VStraight = "VStraight";
    private const string DropLeave = "DropLeave";

    protected override void Awake()
    {
        base.Awake();
        
        // 1
        for (var i = 0; i < 10; ++i)
        {
            AddMinion(Soldier, TopTransforms[-3], VSlide1);
            CurrentTime += 0.25f;
        }

        for (var i = 0; i < 10; ++i)
        {
            AddMinion(Soldier, TopTransforms[-3], VSlide1);
            AddMinion(Soldier, TopTransformsFlipped[3], VSlide1);
            CurrentTime += 0.25f;
        }

        // 2
        CurrentTime += 2.0f;

        AddMinion(SergeantDouble, TopTransformsFlipped[3], DropLeave);
        CurrentTime += 1f;
        AddMinion(SergeantSingle, TopTransforms[-3], DropLeave);
        CurrentTime += 1f;
        AddMinion(SergeantSingle, TopTransformsFlipped[1], DropLeave);
        CurrentTime += 1f;
        AddMinion(SergeantDouble, TopTransforms[-1], DropLeave);
        CurrentTime += 1f;
        AddMinion(SergeantDouble, TopTransformsFlipped[4], DropLeave);
        CurrentTime += 0.75f;
        AddMinion(SergeantSingle, TopTransforms[-4], DropLeave);
        CurrentTime += 0.75f;
        AddMinion(SergeantSingle, TopTransformsFlipped[2], DropLeave);
        CurrentTime += 0.75f;
        AddMinion(SergeantDouble, TopTransforms[-2], DropLeave);
        CurrentTime += 0.75f;
        AddMinion(SergeantDouble, TopTransformsFlipped[3], DropLeave);
        CurrentTime += 0.5f;
        AddMinion(SergeantSingle, TopTransforms[-1], DropLeave);
        CurrentTime += 0.5f;
        AddMinion(SergeantSingle, TopTransformsFlipped[4], DropLeave);
        CurrentTime += 0.5f;
        AddMinion(SergeantDouble, TopTransforms[-2], DropLeave);
        CurrentTime += 0.5f;
        AddMinion(SergeantDouble, TopTransforms[0], DropLeave);
        CurrentTime += 0.25f;
        AddMinion(SergeantSingle, TopTransforms[-3], DropLeave);
        CurrentTime += 0.25f;
        AddMinion(SergeantSingle, TopTransformsFlipped[3], DropLeave);
        CurrentTime += 0.25f;
        AddMinion(SergeantDouble, TopTransforms[-1], DropLeave);
        CurrentTime += 0.25f;
        AddMinion(SergeantDouble, TopTransformsFlipped[1], DropLeave);
    }
}