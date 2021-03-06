# <p align="center">Game engine specific optimization techniques for Unity</p>

## <p align="center">Abstract</p>

When we are developing real-time applications performance is a crucial part of the development process. Unfortunately, the knowledge how to handle performance issues or how to accurately identify the root causes of it are often missing from the developers. In game development, identifying and handling these issues are moreover even harder and a broader topic since a game is usually deployed to many platforms and consists of the work of many fields like art, audio or storytelling.
In this thesis we will learn how and when in the development process we should define our target hardware and what kind of optimization/performance goals we should assign to the project. After this we will deep dive into how the popular real-time development platform Unity works, concentrating especially on its scripting. We will identify which parts of it are performance critical and therefore need special attention from the developers. Afterwards we will gather what type of tools are available for performance analysis in Unity and how to use these tools to properly do benchmarks and performance tests in order to ensure smooth gaming experience for our target audience. At the end of this thesis, we will examine some advanced Unity specific optimization technique and understand why and when they perform better.

## Some images and benchmarks from the thesis

### Update Manager vs Traditional Update Methods

Update manager optimization technique is a special technique which avoids unnecesary interop update calls by using a managed only UpdateManager class.

**Traditional**
<img src="Thesis/Images/Traditional.gif">
**Update Manager**
<img src="Thesis/Images/Manager.gif">

**Benchmarks of the Traditional and Update Manager solution with 10 000 cubes moving up and down**
![Traditional vs Update Manager Benchmarks](Thesis/Images/UpdateManager%20vs%20Traditional%20Interop%20Call%20Benchmark.png?raw=true "Traditional vs Update Manager Benchmarks")

### Performance testing examples

**The dragon level example scene**
<img src="Thesis/Images/dragon.gif">

**Automatic performance validation based on performance requirements**
```cs
[UnityTest, Performance]
public IEnumerator SpawnFireBalls_RecommendedConfig_Min120FPS()
{
    // Arrange
    var fireBallSpawner = GameObject.Find("Spawner").GetComponent<FireBallSpawner>();
    // Small warmup before measurement starts
    yield return new WaitForSecondsRealtime(2.0f);
    // Simulating user input delay
    var userInputDelayYieldInstruction = new WaitForSecondsRealtime(0.15f);
           
    // Act
    using (Measure.Frames().Scope("Frame Time"))
    using (ScopedFPSMeasurement.StartFPSMeasurement("FPS"))
    {
        for (int i = 0; i < 250; i++)
        {
            fireBallSpawner.SpawnFireBalls(25);
            yield return userInputDelayYieldInstruction;
        }
    }

    // Assert
    // Calculating the results for assertions
    PerformanceTest.Active.CalculateStatisticalValues();
    var fpsResults = PerformanceTest.Active.SampleGroups.Find(s => s.Name == "FPS");

    Assert.GreaterOrEqual(fpsResults.Median, 120,
        "Violation of OG_117650: The median FPS should be higher than 120 frames per second.");
}
```

If you are intrested in more bechnmarks and techniques for handling optimization degradations read my thesis :)

## How to run the performance tests on your machine

- Open the project inside Unity
- Go to Window > General > Test Runner
- Select Play Mode tests
- Click on the button in the upper right corner  (Run all in player)
