using System;
using System.Collections.Generic;
using System.IO;
using JiebaNet.Segmenter;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;

namespace MVC_DotNet.Common
{
    
    public class LuceneHelper{
        public  static void createIndex(string title, string content)
        {
            
            //创建Lucene索引文件
            // string IndexDic = @"D:\Lucene\post\";
           
            // IndexWriter writer = new IndexWriter(FSDirectory.Open(IndexDic), new JiebaAnalyzer(), true, IndexWriter.MaxFieldLength.LIMITED);
            // Document doc = new Document();
            // Field postid = new Field("PostId", "1", Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.NO);
            // Field title1 = new Field("Title", "title", Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.NO);
            // Field postscore = new Field("PostScore", "content", Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.NO);
            //     doc.Add(postid);
            //     doc.Add(title1);
            //     doc.Add(postscore);
            //     writer.AddDocument(doc);
            // writer.Optimize();
            // writer.Commit();
            
            // LN.Analysis.Analyzer analyzer = new LN.Analysis.Standard.StandardAnalyzer();
            // LN.Index.IndexWriter iw = new LN.Index.IndexWriter("Index", analyzer, false);
            // LN.Documents.Document document = new LN.Documents.Document();
            // document.Add(new LN.Documents.Field("title", title, LN.Documents.Field.Store.YES, LN.Documents.Field.Index.TOKENIZED));
            // document.Add(new LN.Documents.Field("content", content, LN.Documents.Field.Store.YES, LN.Documents.Field.Index.TOKENIZED));
            // iw.AddDocument(document); iw.Optimize(); iw.Close();
        }
        //  static List<Item> search(string keyWord)
        // {
        //     List<Item> results = new List<Item>();
        //     LN.Analysis.Analyzer analyzer = new LN.Analysis.Standard.StandardAnalyzer();
        //     LN.Search.IndexSearcher searcher = new LN.Search.IndexSearcher("Index");
        //     LN.QueryParsers.MultiFieldQueryParser parser = new LN.QueryParsers.MultiFieldQueryParser(new string[] { "title", "content" }, analyzer);
        //     LN.Search.Query query = parser.Parse(keyWord);
        //     LN.Search.Hits hits = searcher.Search(query);
        //     for (int i = 0; i < hits.Length(); i++)
        //     {
        //         LN.Documents.Document doc = hits.Doc(i);
        //         results.Add(new Item() { Title = doc.Get("title"), Content = doc.Get("content") });
        //     } searcher.Close();
        //     return results;
        // }
    }

}