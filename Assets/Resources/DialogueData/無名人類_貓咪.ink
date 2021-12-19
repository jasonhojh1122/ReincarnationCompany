-> Main

=== Main ===
 
 「喵~」
 
-> Choices

=== Choices ===
 有什麼事情嗎？
+ [ 喵 ] -> Meow
+ [ 聊天 ] -> Chat
   
=== Meow ===
喵喵~
 -> END
 
=== Chat ===
喵~
    -> Choices2

=== Choices2 ===
 有什麼事情嗎？
+ [ 喵 ] -> Meow
+ [ 聊天 ] -> Chat2   

=== Chat2 ===
喵~
    -> Choices3
    
=== Choices3 ===
 有什麼事情嗎？
+ [ 喵 ] -> Meow
+ [ 聊天 ] -> Chat3    
    
=== Chat3 ===

「人類，不要再煩我了，快去賺錢 ^ Φ ω Φ ^」
 -> Talk

===Talk===
+ [ 「原來你會說話」 ] -> talk2
+ [ 「好...好可愛」 ] -> Cute  

===talk2===
「我只是不想跟白癡人類講話而已 ^ Φ ω Φ ^」
-> Stupid

===Stupid===
（可惡...居然說我是白癡人類...）
（但貓咪真的好可愛喔）
->END

===Cute===
覺得我可愛還不上貢一些食物？# CountConversation
我最喜歡魚了 ^ Φ ω Φ ^
    -> END
