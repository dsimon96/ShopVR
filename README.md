# ShopVR
A bold new way to shop.

*Inspiration*

Created by Cyrus Tabrizi, Melody Ting, David Simon, and Zeleena Kearney at the University of Michigan for MHacks 2016, ShopVR explores the potential for virtual reality as an integrated experience in ordinary life. We played with the idea of using VR as a means of enhancing the online shopping experience and as a way to make something as ordinary as getting groceries easier and more convenient without sacrificing the experience of interacting with the products. 

*How We Made It*

Using the Target API, Zeleena Kearney extracted a variety of item names, prices, and descriptions with Python and HTML and organized the information into files. The actual virtual environment was rendered by Melody Ting in Sketchup and exported to the Unity game engine, where she established basic first person controls,UI, and other interactive features. David Simon then integrated the product information into the game engine such that when the user picks up an item and drops it into his/her basket, the product price and description is updated in the list. This list can then be exported to a mobile app grocery list. Finally, Cyrus Tabrizi created a unique arduino-powered glove that senses the user's gestures and allows them to virtually "grab" items off shelves.

