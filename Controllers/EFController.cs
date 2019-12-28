using System.Collections.Generic;
using JiebaNet.Segmenter;
using JiebaNet.Segmenter.PosSeg;


namespace MVC_DotNet.Controllers
{
    public class EFController:BaseController
    {
        public string Add(){
            
            return "";
        }
        public IEnumerable<Pair> jieba(string text){
            //http://localhost:5000/ef/jieba?text=%E4%BD%A0%E5%A5%BD%E8%BF%99%E6%98%AF%E9%94%99%E8%AF%AF%E7%9A%84
            JiebaSegmenter segmenter=new JiebaSegmenter();
            PosSegmenter posSegmenter=new PosSegmenter(segmenter);
            IEnumerable<Pair> wordList=posSegmenter.Cut(text);
            return wordList;
        }
    }
}