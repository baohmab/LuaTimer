# LuaTimer
This lua script provides some yields which frame,condition,second,coroutine,signal. I tested on unity3d with xlua

	--This script is for coroutine with yield second,yield coroutine,yield sign.
	--	first, define timer object
		local timer = require 'xlua.Timer-master.timer'
	--and you should call a function 'timer.update' every time on all frame with given two parameters: realTime,scaleTime

## Need calling everyframe

			function update(rt,st)
				timer.update(rt,st) --realTime, scaleTime
			end

# how to use:
		timer.waitFrame()
		timer.waitSecond(3)
		timer:off().waitOR(timer.waitSecond(6),timer.waitSignal("test")):on();
		timer:off().waitAND(timer.waitSecond(6),timer.waitSignal("test")):on();
		timer.waitCoroutine(function(pp)   
			timer:startDebug('Thread3')
			timer.waitSecond(3)													
			timer.signal('a')
			signal 'a'	
			end,10)																	

# Case 1
	timer.run(coroutine.create(function() 
		timer:startDebug('Thread Frame')
		while(true) do
			timer.waitSecond(1)
			timer.waitFrame()
		end
	end))
	

# Case 2
	timer.run(function(pp)		      --unblocking		you can use parameter of function 'timer.run' with two type: function, thread
		timer:startDebug('Thread1')
		timer.run(coroutine.create(function()  --unblocking								
			timer:startDebug('Thread2')
			timer.waitSignal('c')       --waiting for signal 'c'								
			timer.waitSecond(10)														
			print("wonderful!!!")														
		end))
		timer.waitSecond(3)	          --waiting for 3 seconds										
		timer.waitSignal('b')         --waiting for signal 'b'									
		timer.waitCoroutine(function(pp)   --waiting for new coroutine until finish		
			timer:startDebug('Thread3')
			timer.waitSecond(3)															
			timer.signal('a')           --send signal 'a' and wake up things waiting signal 'a'	
		end,10)																			
		print("it's very nice!")														
	end,432)
	timer.run(coroutine.create(function(pp) 	--unblocking    you can use parameter of function 'timer.run' with two type: function, thread				
		timer:startDebug('Thread4')
		timer.waitSecond(5)           --waiting for 5 seconds										
		timer.signal('b')             --send signal 'b' and wake up things waiting signal 'b'		
		timer.waitSignal('a')         --waiting for signal 'a'									
		timer.signal('c')             --send signal 'c' and wake up things waiting signal 'c'		
		print("Awesome!!!")																
	end),123)
	
	
# Case 3
	timer.run(function()
		timer:off().waitOR(
			timer.waitSignal("CreatedRoomSuccess"),
			timer.waitSecond(2)
		):on();						
		if timer:checkid('CreatedRoomSuccess')==false then 
		--timer.waitSecond(2)
		else
		--timer.waitSignal("CreatedRoomSuccess"),
		end
	end)
	
