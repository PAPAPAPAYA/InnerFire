
// v0.1
// general systems
	// xshow available interviewees
	// xselect interviewee
	// xunlock new interviewee
		// xunlock new interviewee in cardless dialogue
		// xunlock new interviewee in card dialogue
	// xadvance from day 1 to day 2
	// *add an intro and a transition between day 1 and day 2
	// xlet the player look at the card even when they can't use them
	// *after player gain many cards, how to sort them?
	// x显示中文
	// *让玩家可以在弹出选项后后退？
	// x没有card dialogue的时候不要触发显示按键
	// 没有threaten/trade选项隐藏按钮
	// 如果relationship已经是-1,无法继续减,如果是1,无法继续加
	// 用过卡牌后能重复使用吗?
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
		// 根据关系
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
	// xuse character's defualt dialogue after selecting interviewee, not the one in dialogueStruct
	// xdon't let player to threaten the same character with the same card more than once
		// xrecord approach on card and character it was used on 
	// 目前的字体库字体太少
// bus
	// xexiting the interview will not hide question options
	// xexiting the interview and reenter will spawn player even cardless dialogues haven't been finished
	// xafter unlocking a new chara from a chara, reentering that chara and exit will arrange the characters in a strange way
	// x进入采访后一直点人物会在选项出来之后但是没有选之前就显示返回按键
	// cardless dialogue时，不进行选择直接点击人物能够直接去到下一个cardless dialogue