/**
 * CodeView jQuery Plugin - To View The Code With Beautiful Style
 * http://fstoke.net/jquery-codeview/
 * Copyright (c) David Hung
 * Author: David Hung
 * Version: 1.0
 * Last Revision: 2009-08-06
 * Requires jQuery v1.3.2 or later
 *
 * CodeView has been tested in the following browsers:
 * - IE eSobi(IE7), 8
 * - Firefox 3.5
 * - Opera 9.64
 * - Safari 4.0
 * - Chrome 2.0
 *
 * Note:
 * Currently, it only support javascript lanugage syntax color style.
 * But maybe it will support program language(e.g. java, php, ruby) in the future.
 *
 * This is a open-source jquery plugin. Please feel free to use it any where.
 * Forgive my poor enlish description or comment, because I am not a english speaker. :P
 * Enjoy it!! :^)
 *
 * My Blog: http://fstoke.net/blog/
 */
$.fn.codeview = function(options){
	var caller = $(this);
	var cv = new CodeView(caller, options);
	cv.init();
	return cv;
};

function CodeView(caller, options) {
	var me = this;
	var orgContent = caller.html();
	var content = caller.html();
	var previewContent = null;
	var options = jQuery.extend({
		language: "javascript",	// program language type, currently, only support javascript syntax
		fontSize: "16px",		// css font size style of code text
		tabToWhite: 3,			// transform tab to n-count white space, if -1 means don't transform tab character
		showLog: false			// Show log in firebug console
	}, options);

	me.init = function() {
		//alert(content);
		caller.css("font-size", options.fontSize );
		filterCharacter();
		removeLeadingSpace();
		feedColor();
	};

	me.showHtmlEntity = function() {
		var str = "";
		for( var i=30; i<70; i++ ) {
			str += "&#38;&#35;"+i+";  =  &#"+i+"<br>";
		}
		return str;
	};

	// filter character, regexp: /{string}/{attribute}
	// regular expression attribute - g: global search
	//                                i: csae insensitive
	//                                m: multiline
	// \t    =  tab
	// &lt;  =  <
	// &gt;  =  >
	// &amp; =  &
	// &quot; = "
	//
	// #60;  =  <
	// #62;  =  >
	// #38;  =  &
	// #34;  =  "
	// #39;  =  '
	var filterCharacter = function() {
		content = content.replace(/&lt;/g, '&#60;');
		content = content.replace(/&gt;/g, '&#62;');
		content = content.replace(/&amp;/g, '&#38;');
		content = content.replace(/&quot;/g, '&#34;');

		//content = content.replace(/&/gm, '&#38;');
		content = content.replace(/</gm, '&#60;');
		content = content.replace(/>/gm, '&#62;');
		content = content.replace(/\"/gm, '&#34;');
		content = content.replace(/\'/gm, '&#39;');

		content = content.replace(/\r?\n/g, '<br>');
		//content = content.replace(/<br><br>/g, '<br>');
		content = content.replace(/ /g, '&nbsp;');

		// transform tab to white space
		if( options.tabToWhite != null && options.tabToWhite >= 0 ) {
			var tgtString = "";
			for( var i=0; i<options.tabToWhite; i++ ) {
				tgtString += " ";
			}
			content = content.replace(/\t/g, tgtString);
		}

		caller.html(content);
	};

	var removeLeadingSpace = function() {
		var lines = content.split("<br>");
		var leadingSpace = "";
		for( var i=0; i<lines.length; i++ ) {
			if( $.trim(lines[i]) != "" ) {
				var pos = lines[i].search(/\S/g); // find non-space character position
				leadingSpace = lines[i].substr(0, pos);
				break;
			}
		}
		//alert("["+leadingSpace+"]");
		if( leadingSpace != "" ) {
			content = "";
			for( var i=0; i<lines.length; i++ ) {
				var l = lines[i];
				var pos = lines[i].search(/\S/g); // find non-space character position
				var leadingStr = lines[i].substr(0, pos);
				var tailingStr = lines[i].slice(pos);
				leadingStr = leadingStr.replace(leadingSpace, "");
				//alert("["+leadingStr+"]");
				var appendStr = leadingStr + tailingStr;
				if( i != lines.length-1 || (i == lines.length-1 && checkOnlyWhiteSpace(appendStr) == false) ) {
					if( i > 0 ) {
						content += "<br>";
					}
					content += appendStr;
				}
			}
		}

		caller.html(content);
	};

	var checkOnlyWhiteSpace = function(str) {
		var checkstr = str.replace(/&nbsp;/g, ' ');
		if( $.trim(checkstr) == "" ) {
			return true;
		}
		return false;
	};

	var feedColor = function() {
		// feed keyword color
		// function
		content = content.replace(/(&nbsp;function)/g, wrapKeywordText(" function"));
		content = content.replace(/(function&nbsp;)/g, wrapKeywordText("function "));

		// var
		content = content.replace(/(&nbsp;var)/g, wrapKeywordText(" var"));
		content = content.replace(/(var&nbsp;)/g, wrapKeywordText("var "));

		// try
		content = content.replace(/(&nbsp;try)/g, wrapKeywordText(" try"));
		content = content.replace(/(try&nbsp;)/g, wrapKeywordText("try "));

		// catch
		content = content.replace(/(&nbsp;catch)/g, wrapKeywordText(" catch"));
		content = content.replace(/(catch&nbsp;)/g, wrapKeywordText("catch "));

		// finally
		content = content.replace(/(&nbsp;finally)/g, wrapKeywordText(" finally"));
		content = content.replace(/(finally&nbsp;)/g, wrapKeywordText("finally "));

		// true
		content = content.replace(/(\(true)/g, "("+wrapKeywordText("true"));
		content = content.replace(/(true\))/g, wrapKeywordText("true")+")");
		content = content.replace(/(&nbsp;true&nbsp;)/g, wrapKeywordText(" true "));
		content = content.replace(/(true;)/g, wrapKeywordText("true")+";");

		// false
		content = content.replace(/(\(false)/g, "("+wrapKeywordText("false"));
		content = content.replace(/(false\))/g, wrapKeywordText("false")+")");
		content = content.replace(/(&nbsp;false&nbsp;)/g, wrapKeywordText(" false "));
		content = content.replace(/(false;)/g, wrapKeywordText("false")+";");

		// while
		content = content.replace(/(while&nbsp;)/g, wrapKeywordText("while "));
		content = content.replace(/(while\()/g, wrapKeywordText("while")+"(");

		// for
		content = content.replace(/(if&nbsp;)/g, wrapKeywordText("if "));
		content = content.replace(/(if\()/g, wrapKeywordText("if")+"(");

		// return
		content = content.replace(/(&nbsp;return)/g, wrapKeywordText(" return"));
		content = content.replace(/(return&nbsp;)/g, wrapKeywordText("return "));

		// { and }
		content = content.replace(/({)/g, wrapKeywordText("{"));
		content = content.replace(/(})/g, wrapKeywordText("}"));

		// feed comment color: [// xxx]
		var lines = content.split("<br>");
		content = "";
		for( var i=0; i<lines.length; i++ ) {
			var l = lines[i];
			var pos = l.indexOf("//");
			var offsetPos = 0;
			while( pos > 0 && l.charAt(pos-1) == ':' ) {
				var nextStr = l.substr(pos+2);
				offsetPos += pos+2;
				pos = nextStr.indexOf("//");
				//alert(pos+','+offsetPos);
			}
			if( pos >= 0 ) {
				pos += offsetPos;
				var remain = l.substr(pos);
				l = l.substr(0, pos) + "<code_comment>" + remain + "</code_comment>";
			}
			content += l+"<br>";
		}

		// feed comment color: [/* xxx */]
		content = content.replace(/\/\*/g, "<code_comment>/*");
		content = content.replace(/\*\//g, "*/</code_comment>");

		// check comment text, restore the keyword text in comment
		var start = content.indexOf("<code_comment>");
		var end = content.indexOf("</code_comment>");
		var partialContent = content;
		var prefixContent = "";
		while( start >= 0 && end > start ) {
			log("partialContent:\r\n"+partialContent);

			var commentText = partialContent.slice(start, end + "</code_comment>".length);
			commentText = restoreKeywordText(commentText);
			log("commentText:\r\n"+commentText);

			prefixContent += partialContent.slice(0, start) + commentText;
			log("prefixContent:\r\n"+prefixContent);

			partialContent = partialContent.slice(end + "</code_comment>".length);
			var start = partialContent.indexOf("<code_comment>");
			var end = partialContent.indexOf("</code_comment>");
			log(start+','+end);
		}
		content = prefixContent + partialContent;

		//alert(content);

		caller.html(content);
	};

	var wrapKeywordText = function(text) {
		return "<code_keyword>"+text+"</code_keyword>";
	};

	var wrapStringText = function(text) {
		return "<code_string>"+text+"</code_string>";
	};

	var restoreKeywordText = function(text) {
		text = text.replace(/<code_keyword>/g, "");
		text = text.replace(/<\/code_keyword>/g, "");
		return text;
	};

	var log = function(msg) {
		if(options != null && options.showLog && this.console != null) {
			this.console.log(msg);
		}
	};
}