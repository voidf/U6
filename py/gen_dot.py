import math
import tkinter as tk
from tkinter import ttk
from PIL import Image, ImageDraw, ImageTk

import numpy as np

def draw_image(radius, spacing, phase, canvas_size):
    # Create an image to draw on
    image = Image.new("RGBA", canvas_size, (255,255,255))
    draw = ImageDraw.Draw(image)
    
    # Adjust phase to be within a reasonable range
    effective_phase = phase * 0.5 * spacing
    
    # Calculate the number of dots based on the canvas size and spacing
    num_cols = (canvas_size[0] + spacing) // spacing * 2
    num_rows = (canvas_size[1] + spacing) // spacing * 2
    
    # Draw dots
    for row in range(num_rows):
        row_phase = effective_phase if row % 2 == 1 else 0
        for col in range(num_cols):
            x = col * spacing + row_phase
            y = row * (1 if not effective_phase else math.sqrt(3) / 2) * spacing
            draw.ellipse((x - radius, y - radius, x + radius, y + radius), fill=(0,0,0,255))
    
    print(np.array(image))
    return image

def draw_dots(canvas: tk.Canvas, radius, spacing, phase, canvas_size):
    # Avoid invalid configurations
    if radius < 1 or spacing <= 2 * radius:
        print('Invalid configuration!', 'radius:', radius, 'spacing:', spacing)
        return

    # Clear the canvas
    canvas.delete("all")

    image = draw_image(radius, spacing, phase, canvas_size)
    tk_image = ImageTk.PhotoImage(image)
    canvas.create_image(0, 0, image=tk_image, anchor="nw")
    canvas.image = tk_image  # Keep a reference!

def save_image(radius, spacing, phase, canvas_size, filename="output.png"):
    image = draw_image(radius, spacing, phase, canvas_size)
    image.save(filename)

def create_gui():
    root = tk.Tk()
    root.title("Dot Pattern Generator")

    # Create a canvas for image preview
    canvas = tk.Canvas(root, width=800, height=600)
    canvas.pack()

    # Control frame
    control_frame = ttk.Frame(root)
    control_frame.pack(fill=tk.X, expand=True)

    # Helper function to update the label with the slider value
    def update_label(label, value):
        v = float(value)
        label.config(text=f"{v:.0f}")
    
    # Radius control
    radius_label = ttk.Label(control_frame, text="Radius:")
    radius_label.pack(side=tk.LEFT)
    radius_slider = ttk.Scale(control_frame, from_=1, to=100, orient=tk.HORIZONTAL, value=20)
    radius_value_label = ttk.Label(control_frame, text=f"{radius_slider.get():.0f}")
    radius_slider.pack(side=tk.LEFT, fill=tk.X, expand=True)
    radius_value_label.pack(side=tk.LEFT)
    radius_slider.configure(command=lambda value: update_label(radius_value_label, value))
    
    # Spacing control
    spacing_label = ttk.Label(control_frame, text="Spacing:")
    spacing_label.pack(side=tk.LEFT)
    spacing_slider = ttk.Scale(control_frame, from_=20, to=200, orient=tk.HORIZONTAL, value=50)
    spacing_value_label = ttk.Label(control_frame, text=f"{spacing_slider.get():.0f}")
    spacing_slider.pack(side=tk.LEFT, fill=tk.X, expand=True)
    spacing_value_label.pack(side=tk.LEFT)
    spacing_slider.configure(command=lambda value: update_label(spacing_value_label, value))
    
    # Phase control
    phase_label = ttk.Label(control_frame, text="Phase:")
    phase_label.pack(side=tk.LEFT)
    phase_slider = ttk.Scale(control_frame, from_=0, to=1, orient=tk.HORIZONTAL, value=0)
    phase_value_label = ttk.Label(control_frame, text=f"{phase_slider.get():.0f}")
    phase_slider.pack(side=tk.LEFT, fill=tk.X, expand=True)
    phase_value_label.pack(side=tk.LEFT)
    phase_slider.configure(command=lambda value: update_label(phase_value_label, value))

    # Canvas size controls
    canvas_width_label = ttk.Label(control_frame, text="Canvas Width:")
    canvas_width_label.pack(side=tk.LEFT)
    canvas_width_slider = ttk.Scale(control_frame, from_=100, to=1600, orient=tk.HORIZONTAL)
    canvas_width_slider.set(800)  # default value
    canvas_width_slider.pack(side=tk.LEFT, fill=tk.X, expand=True)

    canvas_height_label = ttk.Label(control_frame, text="Canvas Height:")
    canvas_height_label.pack(side=tk.LEFT)
    canvas_height_slider = ttk.Scale(control_frame, from_=100, to=1200, orient=tk.HORIZONTAL)
    canvas_height_slider.set(600)  # default value
    canvas_height_slider.pack(side=tk.LEFT, fill=tk.X, expand=True)
    
    # Update button
    update_button = ttk.Button(control_frame, text="Update Preview",
                               command=lambda: draw_dots(
                                   canvas,
                                   int(radius_slider.get()),
                                   int(spacing_slider.get()),
                                   int(phase_slider.get()),
                                   (int(canvas_width_slider.get()), int(canvas_height_slider.get()))
                               ))
    update_button.pack(side=tk.LEFT)

    # Save button
    save_button = ttk.Button(control_frame, text="Save Image",
                             command=lambda: save_image(
                                 int(radius_slider.get()),
                                 int(spacing_slider.get()),
                                 int(phase_slider.get()),
                                 (int(canvas_width_slider.get()), int(canvas_height_slider.get()))
                             ))
   
    save_button.pack(side=tk.LEFT)

    # Initialize preview with default settings
    draw_dots(
        canvas,
        int(radius_slider.get()),
        int(spacing_slider.get()),
        int(phase_slider.get()),
        (int(canvas_width_slider.get()), int(canvas_height_slider.get()))
    )

    # Run the GUI
    root.mainloop()

# Create and run the GUI
create_gui()
