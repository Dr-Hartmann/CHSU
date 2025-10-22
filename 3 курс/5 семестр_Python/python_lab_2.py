import turtle 
turtle.reset()

turtle.speed(10)
turtle.hideturtle()

for y in range(1,5):
    match y:
        case 1:
            turtle.pencolor("#FF4500")
            turtle.color("#FF4500")
        case 2:
            turtle.pencolor("#FF00FF")
            turtle.color("#FF00FF")
        case 3:
            turtle.pencolor("#000000")
            turtle.color("#000000")
        case 4:
            turtle.pencolor("#00FFFF")
            turtle.color("#00FFFF")
    
    turtle.begin_fill()

    for i in range(5):
          turtle.right(144) 
          turtle.forward(100*y)
    
    turtle.end_fill()
    
num_sides = 5
side_length = 247
angle = 360.0 / num_sides  

turtle.right(angle*1.5) 
turtle.pencolor("#000000")
  
for i in range(num_sides): 
    turtle.forward(side_length) 
    turtle.right(angle) 

turtle.exitonclick()

# turtle.forward(100)
# turtle.right(90)
# turtle.forward(50)
# turtle.right(180)
# turtle.forward(50)
# turtle.left(90)
# turtle.forward(100)
# turtle.left(90)