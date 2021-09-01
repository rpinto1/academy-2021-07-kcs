using System;
using System.Collections.Generic;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Pocos
{
    public class NewsData
    {
        public NewsSource Source { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string UrlToImage { get; set; }

        public string PublishedAt { get; set; }

        public string Content { get; set; }

    }

    public class NewsSource
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }
}


/*
 
            "source": {
                "id": null,
                "name": "Jornaldenegocios.pt"
            },
            "author": "Margarida Peixoto",
            "title": "Governo tem de acomodar mais dois mil milhões no OE 2022 mesmo sem tomar novas medidas - Jornal de Negócios",
            "description": "Ministério das Finanças já entregou o Quadro de Políticas Invariantes à Assembleia da República. Documento identifica despesas e poupanças adicionais para o próximo Orçamento do Estado, face a 2021.",
            "url": "https://www.jornaldenegocios.pt/economia/conjuntura/detalhe/governo-tem-de-acomodar-mais-dois-mil-milhoes-no-oe-2022-mesmo-sem-tomar-novas-medidas",
            "urlToImage": "https://cdn.jornaldenegocios.pt/images/2021-07/img_1200x676$2021_07_13_10_12_05_407675.jpg",
            "publishedAt": "2021-09-01T10:23:58Z",
            "content": null
 
 */