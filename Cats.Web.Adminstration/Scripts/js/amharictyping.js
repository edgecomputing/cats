// Copyright: (c) 2009 by SelamSoft Inc.
// Product title: Amharic Phonetic Typing Javascript
// Product version details: 2.1.0, 01-28-2009  Updated and tested on IE, Firefox, Opera, Safari and Google Chrome
// Product URL: http://www.amharicdictionary.com/js/amharictyping.js
// Contact info: matt@selamsoft.com
// Notes: You are free to use this script on your website with the requirement that you 
//        link to www.AmharicDictionary.com on your site's front page. 
// Feel free to copy, use and change this script as long as this head part remains unchanged.  
// If you improve on this script, please send us your updates.


function getCursorPosition(){ 
	var range;
	range = document.selection.createRange();
	range.moveStart("Textedit", -1);
	return range.text.length;
};

function isRootLetter(amharicValue)
{
	var isRoot = false;
	
	switch (amharicValue)
	{
		case 4629:
		case 4677:
		case 4741: 
		case 4797:
		case 4813: 
		case 4653: 
		case 4845: 
		case 4725: 
		case 4901: 
		case 4949: 
		case 4661: 
		case 4853: 
		case 4941: 
		case 4821:
		case 4877: 
		case 4613: 
		case 4869: 
		case 4781: 
		case 4621: 
		case 4829: 
		case 4933: 
		case 4733: 
		case 4717: 
		case 4709: 
		case 4765: 
		case 4757: 
		case 4637: 
		case 4909: 
		case 4637: 
		case 4917: 
		case 4669: 
		case 4925:
		case 4645:
		case 4837: 
		case 4637: 
		{
			isRoot = true;
			break;
		}
	}
	return isRoot;
};

function withinRange(numValue)
{
	if ( numValue >= 4608 && numValue <= 4988)
	{
		return true;
	} else{
		return false;
	}
};

function AmharicPhoneticKeyPress(pressEvent, amharic){
	var keyCode = 0
	var isNetscape = false;
	var range;
	var newPos;
	var startPos = 0;
	var endPos = 0;
	
	if(pressEvent.which) 
	{
		// netscape
		keyCode = pressEvent.which; 
		isNetscape = true;
	}
	else if(window.event) {
	// for IE, e.keyCode or window.event.keyCode can be used
		keyCode = pressEvent.keyCode; 
		range = document.selection.createRange();
		newPos = getCursorPosition();
		startPos = getCursorPosition();
		endPos = getCursorPosition();
	}

	var shiftKey = false; 
var controlKey = false;
if (window.event)
{ shiftKey = window.event.shiftKey; controlKey=window.event.ctrlKey;} else if (pressEvent.which)
{ shiftKey = pressEvent.shiftKey; controlKey=pressEvent.ctrlKey;}
if( controlKey) return true;


	var keyString = String.fromCharCode(keyCode).toLowerCase();	
	var rawString = String.fromCharCode(keyCode);
	
	// let amharic entry pass.
	// don't let quote pass - this is for Tebbeq - ascii 34
	if ( keyCode > 4000 || (keyCode == 33) || (keyCode >= 35 && keyCode <= 57 && keyCode != 44 && keyCode != 46) || (keyCode >= 91 && keyCode <= 93) || (keyCode >= 60 && keyCode <= 64) || (keyCode >= 123 && keyCode <= 125))
	{
		return true;
	} 


	if (amharic.selectionStart || amharic.selectionStart == '0') 
	{
		startPos = amharic.selectionStart;
		endPos = amharic.selectionEnd;
		newPos = startPos;
	} 
	
	var lastCharPlain = amharic.value.charCodeAt(startPos-1);
	 

	if (keyCode != 16 && keyCode != 8 && keyCode != 13 && keyCode != 0 && keyCode != 32)//&& keyCode != 116) //shift)
	{
		var fieldValue = "";
		var amharicValue = keyString;
		var tempExtraLetter = amharic.value.substring(0,amharic.value.length - 1);
		
		var consonantRetVal = convertEnglishConsonantToAmharic(lastCharPlain, amharicValue, shiftKey);

		if (consonantRetVal != null && consonantRetVal.length > 0)
		{
			var tempStartPos = startPos;
			if( amharicValue == "h" && shiftKey == false)
			{
				var hconsValue = 0;
				if ( lastCharPlain == 4629)
				{hconsValue = 4741;} 
				else if (lastCharPlain == 4613)
				{hconsValue = 4629;} else
				{hconsValue = 4613; newPos = newPos + 1; tempStartPos = startPos + 1;}

				amharic.value = amharic.value.substring(0, tempStartPos-1)
					+ String.fromCharCode(hconsValue )
					+ amharic.value.substring(startPos, amharic.value.length);
			}else if ( amharicValue == "s" && shiftKey == false)
			{
				var tempStartPos = startPos;
				var hconsValue = 0;
				if ( lastCharPlain == 4661){
					hconsValue = 4645;
				} else {hconsValue = 4661; tempStartPos = startPos + 1; newPos = newPos + 1}
					amharic.value = amharic.value.substring(0, tempStartPos-1)
					+ String.fromCharCode(hconsValue )
					+ amharic.value.substring(startPos, amharic.value.length);			
			} else if (amharicValue == ",")
			{
				var tempStartPos = startPos;
				var hconsValue = 0;
				if ( lastCharPlain == 4963){
					hconsValue = 44;
				} else {hconsValue = 4963; tempStartPos = startPos + 1; newPos = newPos + 1}
					amharic.value = amharic.value.substring(0, tempStartPos-1)
					+ String.fromCharCode(hconsValue )
					+ amharic.value.substring(startPos, amharic.value.length);			
			
			} else if( amharicValue == ".")
			{
				var tempStartPos = startPos;
				var hconsValue = 0;
				if ( lastCharPlain == 4962){
					hconsValue = 46;
				} else {hconsValue = 4962; tempStartPos = startPos + 1; newPos = newPos + 1}
					amharic.value = amharic.value.substring(0, tempStartPos-1)
					+ String.fromCharCode(hconsValue )
					+ amharic.value.substring(startPos, amharic.value.length);			
			}else if (amharicValue == "\"")
		    {
			    amharic.value = amharic.value + String.fromCharCode(4959); // 135F = Tebbeq
			    newPos = newPos + 1;	
		    
		    }
		    else
			{
				amharic.value = amharic.value.substring(0, startPos)
					+ consonantRetVal
					+ amharic.value.substring(endPos, amharic.value.length);
					
				newPos = newPos + 1;
			}	
		} 
		
		var aOffset = -2;
		var eOffset = -5;
		var iOffset = -3;
		var uOffset = -4;
		var oOffset = 1;
		var eeOffset = -1;
		var offset = 0;
		var vowelCharacter ;
		var otherVowelCharacter = 0;
		if ( (amharicValue == "a" && shiftKey == false) || amharicValue == "4") 
		{
			offset = aOffset;
			vowelCharacter = 4768;
			if ( lastCharPlain == 4768) { otherVowelCharacter = 4816;}
		}	else if ( (amharicValue == "a" && shiftKey == true) ) 
		{
			offset = aOffset;
			vowelCharacter = 4771;
			if ( lastCharPlain == 4771 ) { otherVowelCharacter = 4819;}
		}	
		else if ( (amharicValue == "e" && shiftKey == false) || amharicValue == "1")
		{
			offset = eOffset;		
			vowelCharacter = 4773;
			if ( lastCharPlain == 4773 ) { otherVowelCharacter = 4821;}
		}else if ( (amharicValue == "i") || amharicValue == "3")
		{
			offset = iOffset;
			vowelCharacter = 4770;
			if ( lastCharPlain == 4770 ) { otherVowelCharacter = 4818;}
		}else if (( amharicValue == "o") || amharicValue == "7")
		{
			offset = oOffset;
			vowelCharacter = 4774;
			if ( lastCharPlain == 4774 ) { otherVowelCharacter = 4822;}
		}else if ( (amharicValue == "u") || amharicValue == "2")
		{
			offset = uOffset;
			vowelCharacter = 4769;
			if ( lastCharPlain == 4769 ) { otherVowelCharacter = 4817;}
		}else if ( (amharicValue == "e" && shiftKey ==true) || amharicValue == "5")
		{
			offset = eeOffset;
			vowelCharacter = 4772;
			if ( lastCharPlain == 4772 ) { otherVowelCharacter = 4820;}
		}  else if( amharicValue == ";" )
		{
			amharic.value = amharic.value + String.fromCharCode(4964);
			newPos = newPos + 1;	
			
		} else if( amharicValue == ":" )
		{
			amharic.value = amharic.value + String.fromCharCode(4961);
			newPos = newPos + 1;	
		}

		var vowelCharacterChar = String.fromCharCode(vowelCharacter);
			//var cursorIndex = amharic.value.indexOf("|");
			//cursor.text = "" + cursorIndex; 

		var lastCharEtymology = amharic.value.charCodeAt(startPos-1);

		if ( offset != 0 )
		{  //?
			
			if ( amharic.value.length < 1 )
			{
				amharic.value = amharic.value + vowelCharacterChar ;
						newPos = newPos + 1;	

				//pressEvent.keyCode = 4773;
			}else if(amharic.value.charCodeAt(startPos-1) == 32 || lastCharPlain == 4959)  // if tebeq
			{
				amharic.value = amharic.value + vowelCharacterChar ;
						newPos = newPos + 1;	
			
			}
			else if (lastCharEtymology  == 4883 || lastCharEtymology == 4683 || lastCharEtymology == 4803 || lastCharEtymology == 4787 || lastCharEtymology == 4747)
			{
				if ( amharicValue == "e" && shiftKey == false) lastCharEtymology = lastCharEtymology - 3;
				if ( amharicValue == "i") lastCharEtymology = lastCharEtymology - 1;
				//if ( amharicValue == "a") lastCharEtymology = lastCharEtymology ;
				if ( amharicValue == "e" && shiftKey == true) lastCharEtymology = lastCharEtymology + 1;
				if ( amharicValue == "u") lastCharEtymology = lastCharEtymology + 2;

					amharic.value = amharic.value.substring(0, startPos-1)
						+ String.fromCharCode(lastCharEtymology )
						+ amharic.value.substring(startPos, amharic.value.length);
					
				
			} else
			{
				var lastchar = amharic.value.charCodeAt(startPos-1) + offset;
				
				if ( otherVowelCharacter > 0)
				{
					amharic.value = amharic.value.substring(0, startPos-1)
						+ String.fromCharCode(otherVowelCharacter)
						+ amharic.value.substring(startPos, amharic.value.length);				
				}
				else if ( isRootLetter(lastCharPlain)  )
				{ 
					amharic.value = amharic.value.substring(0, startPos-1)
						+ String.fromCharCode(lastchar)
						+ amharic.value.substring(startPos, amharic.value.length);
				} else
				{
					amharic.value = amharic.value.substring(0, startPos)
						+ vowelCharacterChar
						+ amharic.value.substring(startPos, amharic.value.length);		
						
						newPos = newPos + 1;	
							
				}
			}
		}
	
		//Output.value = amharic.value.charCodeAt(startPos-1);
		if ( amharicValue == "w" && shiftKey == true )//amharicValue == "/") // keycode == 191
		{//4775
			var lastchar2 = amharic.value.charCodeAt(startPos-1);

			if (amharic.value.length < 1) 
			{
				lastCharModified = 4775;
				amharic.value = String.fromCharCode(4775)
				newPos = newPos + 1;
			}
			
			
			if( ! (lastchar2 == 4845 || lastchar2 == 4813 || lastchar2 == 4933 || lastchar2 == 4768  || lastchar2 == 4821) )
			{
				
				var lastCharModified;
				if (lastchar2 == 4677 || lastchar2 == 4877  || lastchar2 == 4781  )
				{
					lastCharModified = lastchar2 + 6;
				} else if (lastchar2 == 4613)
				{
					lastCharModified = 4747;
				} else if (lastchar2 == 4797) 
				{
					lastCharModified = 4803;
				}
				else if ( lastchar2 == 4741)
				{
					lastCharModified = 4747;
				} else
				{
					lastCharModified = lastchar2 +2;
				}
				
				if (isRootLetter(lastchar2))
				{
					//Output.value = lastchar2;
					amharic.value = amharic.value.substring(0, startPos-1)
							+ String.fromCharCode(lastCharModified)
							+ amharic.value.substring(startPos, amharic.value.length);

					//amharic.value = amharic.value.substring(0, amharic.value.charCodeAt(startPos-1)) + String.fromCharCode(lastchar)
				}
			} 
		
		} 
	



		if ( isNetscape )
		{
			amharic.setSelectionRange(newPos, newPos); 
		} else
		{
			
			range.collapse();
			range.moveStart("Character", newPos);
			range.select();
		
		}

		return false;
	}
	
	return true;
};

function convertEnglishConsonantToAmharic(lastChar, amharicValue, shiftKey)		{
		var fieldValue;
		var lastcharConsonant = lastChar;

		if( amharicValue == "q" )
		{
			fieldValue = 4677;
		}else if ( amharicValue == "h" && shiftKey == true)
		{
			fieldValue = 4629;		

		} else if ( amharicValue == "k" && shiftKey == true)
		{
			fieldValue = 4797;
		}else if ( amharicValue == "w" && shiftKey == false)
		{
			fieldValue = 4813;
		} else if ( amharicValue == "r")
		{
			fieldValue = 4653;
		}  else if ( amharicValue == "y")
		{
			fieldValue = 4845;
		}else if ( amharicValue == "t" && shiftKey == true)
		{
			fieldValue = 4901;
		}else if ( amharicValue == "t" && shiftKey == false)
		{
			fieldValue = 4725;
		}else if ( amharicValue == "p" && shiftKey == false)
		{
			fieldValue = 4949;
		} else if ( amharicValue == "s" && shiftKey == false)
		{
			fieldValue = 4661;
		} else if ( amharicValue == "d")
		{
			fieldValue = 4853;
		} else if ( amharicValue == "f")
		{
			fieldValue = 4941;
		}else if ( amharicValue == "g")
		{
			fieldValue = 4877;
		}else if ( amharicValue == "h" && shiftKey == false)
		{
			fieldValue = 4613;		
		}else if ( amharicValue == "j")
		{
			fieldValue = 4869;
		}else if ( amharicValue == "k" && shiftKey == false)
		{
			fieldValue = 4781;
		}else if ( amharicValue == "l")
		{
			fieldValue = 4621;
		}else if ( amharicValue == "z" && shiftKey == false)
		{
			fieldValue = 4829;
		}else if ( amharicValue == "z" && shiftKey == true)
		{
			fieldValue = 4837;
		}else if ( amharicValue == "x" && shiftKey == false)
		{
			fieldValue = 4933;
		}else if ( amharicValue == "x" && shiftKey == true)
		{
			fieldValue = 4925;
		}else if ( amharicValue == "c" && shiftKey == false)
		{
			fieldValue = 4733;
		}else if ( amharicValue == "c" && shiftKey == true)
		{
			fieldValue = 4909;
		}else if ( amharicValue == "v")
		{
			fieldValue = 4717;
		}else if ( amharicValue == "b")
		{
			fieldValue = 4709;
		}else if ( amharicValue == "n" && shiftKey == true)
		{
			fieldValue = 4765;
		}else if ( amharicValue == "n" && shiftKey == false)
		{
			fieldValue = 4757;
		}else if ( amharicValue == "m")
		{			//amharic.value = amharic.value +"?" ;
			fieldValue = 4637;
		}else if (amharicValue == "p" && shiftKey == true) //[
		{
			fieldValue = 4917;
		} else if (amharicValue == "s" && shiftKey == true)
		{
			fieldValue = 4669;
			
		} else if( amharicValue == ",")
		{
			fieldValue = 4963;
		}else if( amharicValue == ".")
		{
			fieldValue = 4962;
		}else if( amharicValue == "\"")  // tebeq -tebek
		{
			fieldValue = 4959;
		}
		
		if ( fieldValue )
		{
			return String.fromCharCode(fieldValue);
		} else
		{
			return "";
		}
};


function withinRange(numValue)
{
	if ( numValue >= 4608 && numValue <= 4951)
	{
		return true;
	} else{
		return false;
	}
};