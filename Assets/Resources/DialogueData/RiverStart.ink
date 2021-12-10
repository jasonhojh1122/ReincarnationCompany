-> Main

=== Main ===
這邊是忘川整治局，你應該是要來應徵忘川整治工程的人吧。
-> Choices

=== Choices ===
你要花費100元租借作業船隻嗎?
+ [要]
    好的，讓我檢查一下你的餘額。 # CheckMoney
    -> END
+ [不要]
    不要就請走開，不要浪費彼此的時間了。
    -> END
    
=== EnoughMoney ===
來吧，發揮你的實力吧。
# Enter
->END
    
=== RunOutOfMoney ===
你的錢不夠哦，想辦法去賺點錢再來吧。
-> END
