# LuaTimer
This lua script provide with yield second,yield coroutine,yield sign. I tested on xlua with unity3d

	--This script is for coroutine with yield second,yield coroutine,yield sign.
	--	first, define timer object
		local timer = require 'xlua.Timer-master.timer'
	--and you should call a function 'timer.update' every time on all frame with given two parameters: realTime,scaleTime

	--	for instance 

			function update(rt,st)
				timer.update(rt,st)
			end

	--how to use:

	--< Case 1 Begin>
	timer.run(coroutine.create(function() 
		timer:startDebug('Thread Frame')
		while(true) do
			timer.waitSecond(1)
			timer.waitFrame()
		end
	end))
	--< Case 1 End

	--< Case 2 Begin>
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
	--< Case 2 End
