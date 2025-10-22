x = int(input("Enter a value for x: "))
y = (x-11)**2 - 125
print(y)

import math

# Calculate the two possible values of x
x1 = 11 + math.sqrt(163)
x2 = 11 - math.sqrt(163)

# Print the values of x
print(f"Possible values of x are: {x1} and {x2}")