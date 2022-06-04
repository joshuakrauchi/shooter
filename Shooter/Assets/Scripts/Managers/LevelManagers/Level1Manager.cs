using UnityEngine;

public class Level1Manager : LevelManager
{
    [field: SerializeField] private GameObject Soldier { get; set; }
    [field: SerializeField] private GameObject SergeantSingle { get; set; }
    [field: SerializeField] private GameObject SergeantDouble { get; set; }
    [field: SerializeField] private GameObject LieutenantConstant { get; set; }
    [field: SerializeField] private GameObject LieutenantDouble { get; set; }
    [field: SerializeField] private GameObject General1 { get; set; }
    [field: SerializeField] private GameObject General2 { get; set; }

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

        // 3
        CurrentTime += 5.5f;

        for (var i = 0; i < 10; ++i)
        {
            AddMinion(Soldier, TopTransforms[0], HSlide1);
            AddMinion(Soldier, TopTransformsFlipped[0], HSlide1);

            CurrentTime += 0.25f;
        }

        AddMinion(LieutenantConstant, TopTransforms[0], VDangle);

        for (var i = 0; i < 15; ++i)
        {
            AddMinion(Soldier, TopTransforms[0], HSlide1);
            AddMinion(Soldier, TopTransformsFlipped[0], HSlide1);

            CurrentTime += 0.25f;
        }

        // Boss 1
        CurrentTime += 5.0f;

        AddBoss(General1, new Vector2(0f, GameData.ScreenRect.yMax + 5.0f));

        // 5
        CurrentTime += 5.0f;

        for (var i = 0; i < 5; ++i)
        {
            AddMinion(Soldier, TopTransforms[-3], VSlide1);

            CurrentTime += 0.25f;
        }

        for (var i = 0; i < 5; ++i)
        {
            AddMinion(Soldier, TopTransforms[-3], VSlide1);
            AddMinion(Soldier, TopTransformsFlipped[3], VSlide1);

            CurrentTime += 0.25f;
        }

        for (var i = 0; i < 5; ++i)
        {
            AddMinion(Soldier, TopTransformsFlipped[3], VSlide1);
            AddMinion(Soldier, TopTransforms[0], HSlide1);

            CurrentTime += 0.25f;
        }

        for (var i = 0; i < 5; ++i)
        {
            AddMinion(Soldier, TopTransforms[0], HSlide1);
            AddMinion(Soldier, TopTransformsFlipped[0], HSlide1);

            CurrentTime += 0.25f;
        }

        // 6
        CurrentTime += 4.0f;

        for (var i = 0; i < 5; ++i)
        {
            AddMinion(Soldier, TopTransforms[0], HSlide1);
            AddMinion(Soldier, TopTransformsFlipped[0], HSlide1);

            CurrentTime += 0.25f;
        }

        AddMinion(SergeantDouble, TopTransforms[-3], DropLeave);
        AddMinion(SergeantDouble, TopTransformsFlipped[3], DropLeave);

        for (var i = 0; i < 5; ++i)
        {
            AddMinion(Soldier, TopTransforms[0], HSlide1);
            AddMinion(Soldier, TopTransformsFlipped[0], HSlide1);

            CurrentTime += 0.25f;
        }

        AddMinion(SergeantDouble, TopTransforms[-4], DropLeave);
        AddMinion(SergeantDouble, TopTransformsFlipped[4], DropLeave);

        for (var i = 0; i < 10; ++i)
        {
            AddMinion(Soldier, TopTransforms[0], HSlide1);
            AddMinion(Soldier, TopTransformsFlipped[0], HSlide1);

            CurrentTime += 0.25f;
        }

        // 7
        CurrentTime += 7f;

        AddMinion(SergeantDouble, TopTransformsFlipped[3], DropLeave);
        CurrentTime += 1.0f;
        AddMinion(SergeantSingle, TopTransforms[-3], DropLeave);
        CurrentTime += 1.0f;
        AddMinion(SergeantSingle, TopTransformsFlipped[1], DropLeave);
        CurrentTime += 1.0f;
        AddMinion(SergeantDouble, TopTransforms[-1], DropLeave);
        CurrentTime += 1.0f;
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
        AddMinion(LieutenantDouble, TopTransforms[0], DropLeave);
        CurrentTime += 0.25f;
        AddMinion(SergeantSingle, TopTransforms[-3], DropLeave);
        CurrentTime += 0.25f;
        AddMinion(SergeantSingle, TopTransformsFlipped[3], DropLeave);
        CurrentTime += 0.25f;
        AddMinion(LieutenantDouble, TopTransforms[-1], DropLeave);
        CurrentTime += 0.25f;
        AddMinion(LieutenantDouble, TopTransformsFlipped[1], DropLeave);

        CurrentTime += 7.0f;

        AddBoss(General2, new Vector2(0.0f, GameData.ScreenRect.yMax + 5.0f));

        Debug.Log($"Current level length: {CurrentTime}");
    }
}