# <p align="center">Game engine specific optimization techniques for Unity</p>

## <p align="center">Abstract</p>

When we are developing real-time applications performance is a crucial part of the development process. Unfortunately, the knowledge how to handle performance issues or how to accurately identify the root causes of it are often missing from the developers. In game development, identifying and handling these issues are moreover even harder and a broader topic since a game is usually deployed to many platforms and consists of the work of many fields like art, audio or storytelling.
In this thesis we will learn how and when in the development process we should define our target hardware and what kind of optimization/performance goals we should assign to the project. After this we will deep dive into how the popular real-time development platform Unity works, concentrating especially on its scripting. We will identify which parts of it are performance critical and therefore need special attention from the developers. Afterwards we will gather what type of tools are available for performance analysis in Unity and how to use these tools to properly do benchmarks and performance tests in order to ensure smooth gaming experience for our target audience. At the end of this thesis, we will examine some advanced Unity specific optimization technique and understand why and when they perform better.

## Some images and benchmarks from the thesis

### Update Manager vs Traditional Update Methods

![Traditional](Thesis/Images/Traditional.gif =250x)
![Update Manager](Thesis/Images/Manager.gif =150x)

![Traditional vs Update Manager Benchmarks](Thesis/Images/UpdateManager%20vs%20Traditional%20Interop%20Call%20Benchmark.png?raw=true "Traditional vs Update Manager Benchmarks")

## How to run the performance tests on your machine

- Open the project inside Unity
- Go to Window > General > Test Runner
- Select Play Mode tests
- Click on the button in the upper right corner  (Run all in player)
