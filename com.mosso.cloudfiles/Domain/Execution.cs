
using System;
using com.mosso.cloudfiles.utils;

namespace com.mosso.cloudfiles
{
	
 	public static class StartProcess
	{
		
		public static StartMessage ByLoggingMessage(string startmessage){
			return new StartMessage(startmessage);
		}
	}
	public class StartMessage
	{
		public Execution ThenDoing(Action action)
		{
				return new Execution(action, _startmessage);
		}
		private string _startmessage;
		public  StartMessage(string startmessage)
		{
			_startmessage = startmessage;	
		}
	
 
	}
	public class Execution
	{
		private Action _action;
		private string _startmessage;
		
		public Error<T> AndIfErrorThrownIs<T>() where T: Exception
		{
			return new Error<T>(_action, _startmessage);
		}
		public Execution(Action action, string startmessage){
			
			_action = action;
			_startmessage = startmessage;
		}
				
	}
	public class Error<T> where T: Exception
	{	
		public ErrorAction<T> Do(Action<T> erroraction)
		{
			return new ErrorAction<T>(_startaction, _startmessage, erroraction);
		}
		
		private Action _startaction;
		private string _startmessage;
		
		public Error(Action startaction, string startmessage)
		{
			_startaction = startaction;
			_startmessage = startmessage;
		}
	}
	public class ErrorAction<T> where T: Exception{
		
		private Action _startaction;
		private Action<T> _erroraction;
		private string _startmessage;
		public ErrorMessage<T> AndLogError(string errormessage) 
		{
			return new ErrorMessage<T>(_startaction, _startmessage, _erroraction, errormessage);
		}
		public ErrorAction(Action startaction, string startmessage, Action<T> erroraction){
			_startaction = startaction;
			_startmessage = startmessage;
			_erroraction = erroraction;
		}
	}
	public class ErrorMessage<T>  where T: Exception
	{
		private Action _startaction;
		private Action<T> _erroraction;
		private string _startmessage;
		private string _errormessage;
		
		public void ThenRethrow(bool shouldRethrow)
		{
			Log.Info(this, _startmessage);
			try
			{ 
				_startaction.Invoke();
			}
			catch(T ex)
			{	
				_erroraction.Invoke(ex);	
		    		Log.Error(this, _errormessage, ex);
				if(shouldRethrow)
					throw;
			}
				
		}
		public ErrorMessage(Action startaction, string startmessage, Action<T> erroraction, string errormessage){
			
			_startaction =  startaction;
			_startmessage = startmessage;
			_erroraction = erroraction;
			_errormessage = errormessage;
		}
			
	}
	
}