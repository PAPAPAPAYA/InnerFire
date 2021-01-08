
// iNNER FiRE
// memo:
	// start screen x = 120
	// cutscene x = 60
	// game scene x = 0
	// game over scene x = -60
// x!文本
	// x填写关键信息x
	// x一些人物直接给予关键信息卡牌，所以要增加这些卡牌x
// !音效
// !音乐
// 优化交互
	// x!移动端优化
	// 优化对话选项
		// x记得将长选项拆分到对话里
		// x常时显示人物名称与好感
		// x放大clue board 新卡新人提示
		// x文字从上往下
		// x拉长选项按钮
// general systems
	// xshow available interviewees
	// xselect interviewee
	// xunlock new interviewee
		// xunlock new interviewee in cardless dialogue
		// xunlock new interviewee in card dialogue
	// xadvance from day 1 to day 2
	// x!pregame
	// x!add an intro and a transition between day 1 and day 2
	// xlet the player look at the card even when they can't use them
	// x*after player gain many cards, how to sort them?
	// x显示中文
	// *让玩家可以在弹出选项后后退？
	// x没有card dialogue的时候不要触发显示按键
	// x如果relationship已经是-1,无法继续减,如果是1,无法继续加
	// x所有人物对话完才进入第二天
	// xdebug用: 跳过第一天按钮[S]
	// x显示人物关系
	// xhide cards that can't be used
	// x获得新卡牌需要有提示
	// x!获得新人物需要有提示
	// x!显示人物身份
	// x!tutorial
	// x逐步解锁人物
		//x 记得增加退出采访前心理独白 --> 解锁人物: 加在最后一个cardless dialogue里
	// x!游戏结束
		// x判断每个关键角色的被解锁了的动机数量和作用数量
			// x目前是把int除以2，也就是说共1个情况下，需要1个；共2个情况下，需要2个；共3个情况下，至少需要2个。理论上。
		// x黑屏一个个角色显示他们的结局
		// x根据解锁数量显示不同结局
		// x!填文本
		// x算motive数量
		// x算fire数量
		// !需要留意是否能够触发结局
	// x如无选项, 隐藏
	// x不要clamp好感度
	// clue board
		// x!clamp
		// x采访中不要显示线索整理按钮
		// xshow clue board and hide
		// xlet player cacel connections
		// xlet player exit cut mode
		// xspawn obtained cards
		// xspawn unlocked characters
		// *spawn 火灾环节
		// xcheck which cards can be used on which chara, spawn what text
		// xcard's who i can be used on not set
// 受访者
	// x显示人物名称
	// x加上可否二次采访的选项
	// x加上cardless dialogue的每个选项的触发条件
		// x没有cardless dialogue拥有能触发的选项的话，不激活cardless dialogue manager
		// x每个选项上记录是否满足触发条件
		// x如cardless dialogue没有可触发选项，则跳过该dialogue
	// x没有cardless dialogue的人物不要触发cardless dialogue
	// xquestions before using any cards
	// xdefault dialogue
	// x触发对话
	// x给予卡牌
	// x现在使用完一张卡后，无法继续，需要可以继续使用其他卡，或是原有卡但只触发对话而不会给予新卡
	// xexit to choose state
	// xprocess card
		// xlimit
			// xbugs
				// xcan't show all dialogues
				// xneed to check if the promise is already broken once on this person, if so don't decrease relationship again
		// xdestroy
	// x关系
		// x改变关系
			// xcardless
			// xcard
		// x根据关系
			// x触发不同的对话
				// xcardless
				// xcard
			// x提供不同的卡牌
			// x限制不同的卡牌
			// x影响不同关系
			// x销毁不同的卡牌
// 玩家
	// x显示手牌
	// x使用手牌
	// xshow card info
	// xdrag card
// fixes
	// x记得初始物品
	// xuse character's defualt dialogue after selecting interviewee, not the one in dialogueStruct
	// xdon't let player to threaten the same character with the same card more than once
		// xrecord approach on card and character it was used on 
	// x目前的字体库字体太少
	// x!不要重复添加卡牌到clue board中
	// x!检查是否有重复添加卡牌到玩家手牌中
	// x卡牌被摧毁的同时摧毁clue board上的版本
	// x!第二天的人物要继承第一天的好感度 （不需要了）
	// x!检查一下好感度是否改变了正确数值
	// x!如关系为positive或者negative时没有对应文本，使用neutral文本
		// x!记得留意check一下
	// x交易按键加上“给予”的意思
	// x!交易摧毁的卡为使用的卡
		// xtrading not working
// bugs
	// xexiting the interview will not hide question options
	// xexiting the interview and reenter will spawn player even cardless dialogues haven't been finished
	// xafter unlocking a new chara from a chara, reentering that chara and exit will arrange the characters in a strange way
	// x进入采访后一直点人物会在选项出来之后但是没有选之前就显示返回按键
	// xcardless dialogue时，不进行选择直接点击人物能够直接去到下一个cardless dialogue
	// x貌似是拖动无法被使用的卡牌后, 到其他人物的地方时, dialogue displayer是inactive的
	// x对话时, 询问的对话需要交易才能显示, (农民 / 医生)
	// x触发cardless选项时, 隐藏ddisplayer
	// x显示卡牌手段选项后，隐藏退出采访按钮
	// 区长有张卡会被吞
	// 厂长不触发cardless dialogue
	// 第二天的过场后文字不消失
	// x在clueboard解锁农民
	// x多次对话后，返回键不出现