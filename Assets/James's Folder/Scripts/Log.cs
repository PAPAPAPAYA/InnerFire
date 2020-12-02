
// v0.1
// general systems
	// xshow available interviewees
	// xselect interviewee
	// xunlock new interviewee
		// xunlock new interviewee in cardless dialogue
		// xunlock new interviewee in card dialogue
	// advance from day 1 to day 2
// 受访者
	// xquestions before using any cards
	// xdefault dialogue
	// x触发对话
	// x给予卡牌
	// x现在使用完一张卡后，无法继续，需要可以继续使用其他卡，或是原有卡但只触发对话而不会给予新卡
	// xexit to choose state
	// process card
		// xlimit
			// xbugs
				// xcan't show all dialogues
				// xneed to check if the promise is already broken once on this person, if so don't decrease relationship again
		// xdestroy
	// 关系
		// x改变关系
			// xcardless
			// xcard
		// display different dialogues based on different relationship
			// positive
			// neutral
			// negative
// 玩家
	// x显示手牌
	// x使用手牌
	// xshow card info
	// xdrag card
// fixes
	// xuse character's defualt dialogue after selecting interviewee, not the one in dialogueStruct
	// xdon't let player to threaten the same character with the same card more than once
		// xrecord approach on card and character it was used on 
// bus
	// xexiting the interview will not hide question options
	// xexiting the interview and reenter will spawn player even cardless dialogues haven't been finished
	// xafter unlocking a new chara from a chara, reentering that chara and exit will arrange the characters in a strange way
