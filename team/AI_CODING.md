----CODING WITH AI EXPERIMENTS-----


#### Rachel
What AI I used: **Google Gemini**
What did I use it for: Learning how to write Scene Management Unit/Integration Tests for the Start Scene Panel
Reflections:
- Gemini came in handy when I just wanted to get started with learning about testing frameworks in Unity
- One downfall is that the AI tool sometimes mixed up regular SW unit testing using C# with Unity script tests
  - Also learned that Unity testing framework does not provide ability to create "Mock" dependencies, which make unit testing options for scripts much harder
- One upside is that the AI tool was able to guide me to the correct formatting for creating unity tests, assembly files for testing frameworks, as well as an introductory list of assertions and test statements I could use in my tests
- Steps to take to ensure correct output: I had to feed it my entire script so it could generate some test case ideas. I also had to tell it some compiling issues I ran into upon using some of their written test cases (it originally had a lot of bugs)
- Future use: I think it does great at creating test ideas, but not neccessarily the code itself. Still a useful tool nontheless


#### Jason
I tried out a unity plugin locally called 'text to script generator and code optimizer', which is based on ChapGPT. I used a legacy free version. I tired to create a few scripts to perform functionalities like object movement and debugings. It turned out that the plugin is pertty reliable on simple tasks and would generate C# attributes to create editor tooltips to clearify its fields. However sometimes it would mismatch the existing references or does not use the correct namespace. In other words, it might not be a good idea to let it create scipts that connects with other ones, but still useful to generate examples for single task scripts.

#### Frank
I used Gemini Advanced to generate a tile map. The results were some high-quality concept pictures, but that wasn't my goal. Most of the AI generation (DALLE3, Midjourney, Stable Diffusion, Adobe Firefly) on the market wouldn't work. They either missed certain keywords top-down/2d/pixel/tile-map, or got those right with an artistic style that would not fit.

#### Baige
I used OpenAI, including ChatGPT and Dalle3 to generate C# scripts and relative images in our game. Since none of us in our team is good at art, and I mainly came up with the background story and the introduction part of the game, I used Dalle3 to generate images visualizing my story. During the process, I did nothing about technology but describing my demand to AI. First of all, it creates some images that are indeed related to my background. However, it does not meet my prefections. Then, I keep pointing out the places I wanted that to improve. After a few times, I found that those images provided by AI are not only as same as what I want, but also out of my expectation. In general, I was shocked by the maturity of the AI nowadays. Although it may not be able to achieve our requirements at the first time, I firmly believe that it could make it with more detailed descriptions. In the future, we shuold use AI to help us accomplish our goals.


#### Cindy
I used ChatGPT to help me generate the documentation. I think it can save time compared to writing it manually, especially for large codebases. I also tried to generate the documentation for other teammates' code to help me learn more about Unity and coding. However, I realized that ChatGPT can create some errors while generating documentation, but I can fix it quickly as long as I understand the code.
I also tried Adobe Firefly to generate different sprites just for fun. I used the sprites created by my teammates as the design pattern and wrote what I wanted in words. It was useful for some inspiration but if we do not change the words it just keeps creating a similar design from before.
In general, I think AI can inspire us but we still need some modifications or updates for actual use. Overall, they are useful.

#### Sean

I used Codeium for code completion instead of Github Copilot.
Reflections:
- The tool was mostly useful for writing code similar to already written code. If I was writing something new, it would often generate wrong code. The best way to use it I found was to write pseudocode first so it can understand what to implement.
- I also used it to explain a portion of code and it gave a decent description of the code's functionality, but I wouldn't use it for stuff I didn't already understand.
- I also generated some documentation, which it was very good at and I only had to make minor edits.
- The way it makes diffs for code it writes is nicer to visualise than copilot
- In general, it seems as good if not better than Github Copilot, and it's free so I will be using it from now on.


#### Thomas
        - I used Adobe Firefly to generate different sprite concepts for enemies and other player visuals.
        - I think this tool could be useful for generating concepts as the AI cannot create images in the exact style that our game is using. There are minor errors in the images themselves as they do not adhere to a pixel-by-pixel philosophy. Therefore the AI will be useful for generating concepts but not actual game assets.
        - Adobe Firefly has a feature that allows the importation of an image to use as a basis for the newly generated images. I ran a few tests using the sprites we have already developed for our game, which generated art that was closer to style we want. However, the images still did not use the same pixel structure which is why it should be restricted to concept-only.
