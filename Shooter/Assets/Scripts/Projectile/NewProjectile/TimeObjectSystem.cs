using Unity.Burst;
using Unity.Collections;
using Unity.Core;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class TimeObjectSystem : SystemBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        
        Debug.Log("OnCreate");
    }

    protected override void OnStartRunning()
    {
        base.OnStartRunning();
        
        Debug.Log("StartRUnning");
    }

    protected override void OnUpdate()
    {
        // Assign values to local variables captured in your job here, so that it has
        // everything it needs to do its work when it runs later.
        // For example,
        //     float deltaTime = Time.DeltaTime;

        // This declares a new kind of job, which is a unit of work to do.
        // The job is declared as an Entities.ForEach with the target components as parameters,
        // meaning it will process all entities in the world that have both
        // Translation and Rotation components. Change it to process the component
        // types you want.
        
        
        
        Entities.ForEach((ref ProjectileComponent projectileComponent, ref DynamicBuffer<ProjectileTimeDataBufferElement> timeData) => {
            
            // Implement the work to perform for each entity here.
            // You should only access data that is local or that is a
            // field on this job. Note that the 'rotation' parameter is
            // marked as 'in', which means it cannot be modified,
            // but allows this job to run in parallel with other jobs
            // that want to read Rotation component data.
            // For example,
            //     translation.Value += math.mul(rotation.Value, new float3(0, 0, 1)) * deltaTime;
            
            UpdateTimeData(ref projectileComponent, ref timeData);

        }).Schedule();
    }

    /*private void UpdateTimeData(ref ProjectileComponent projectileComponent, ref DynamicBuffer<ProjectileTimeDataBufferElement> timeData)
    {
        if (projectileComponent.gameState.IsRewinding)
        {
            if (timeData.Length <= 0) return;

            if (projectileComponent.disabledTimeDataRunCount > 0)
            {
                --projectileComponent.disabledTimeDataRunCount;
            }

            //ITimeData timeData = TimeData.Last.Value;
            Rewind(timeData);

            TimeData.RemoveLast();
        }
        else
        {
            Record();

            if (IsDisabled)
            {
                ++DisabledTimeDataRunCount;

                if (DisabledTimeDataRunCount >= MaxData)
                {
                    OnFullyDisabled();
                }
            }
            else
            {
                DisabledTimeDataRunCount = 0;
            }
        }
    }

    protected abstract void Rewind(ITimeData timeData);

    protected abstract void Record();

    protected abstract void OnFullyDisabled();

    protected void AddTimeData(ITimeData timeData)
    {
        while (TimeData.Count >= MaxData)
        {
            TimeData.RemoveFirst();
        }

        TimeData.AddLast(timeData);
    }*/
}
