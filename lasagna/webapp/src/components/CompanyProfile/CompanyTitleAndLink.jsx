import React from 'react'

export default function CompanyTitleAndLink() {

    const companyName = 'Banco de Mexico';
    const baseUrl = `https://www.google.com/search?q="annual+report"+`;
    const fixArr = companyName.split(' ').map(a => a+'+');
    const append = baseUrl+fixArr;
    


//Tambien recibe el nombre por props desde el click de la barra search.

    return (
        
            
        <div>
        
            <h1>Company.Name</h1>
            <a href={append} > <p>Company.Name</p> </a>
            
           


        </div>
    )
}
