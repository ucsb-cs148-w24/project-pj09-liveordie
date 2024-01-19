from flask import Flask, render_template_string
import time

app = Flask(__name__)

@app.route('/')
def hello_world_game():
    return render_template_string('''
        <h1>Hello World!</h1>
        <button id="startButton" onclick="startGame()">Start Game</button>
        <p id="gameStatus"></p>
        <button id="playAgainButton" onclick="playAgain()" style="display:none;">Play Again</button>

        <script>
            function startGame() {
                document.getElementById("startButton").style.display = "none";
                document.getElementById("gameStatus").innerHTML = "Game Started";
                setTimeout(function() {
                    document.getElementById("gameStatus").innerHTML = "Game Over";
                    document.getElementById("playAgainButton").style.display = "block";
                }, 2000);  // Adjust the delay time (in milliseconds) as needed
            }

            function playAgain() {
                document.getElementById("gameStatus").innerHTML = "";
                document.getElementById("playAgainButton").style.display = "none";
                document.getElementById("startButton").style.display = "block";
            }
        </script>
    ''')

# Run the app
if __name__ == '__main__':
    app.run(debug=True)
