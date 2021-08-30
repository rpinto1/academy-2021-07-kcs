import React, { useEffect, useState } from 'react'
import { Dropdown, Segment, Table ,Menu, Icon} from 'semantic-ui-react'
import { Company } from './Company'
import Pagination from './Pagination'

export const IISList = () => {


    const [index, setindex] = useState([{key: "", text: "", value: ""}])
    const [indexValue, setindexValue] = useState("")
    const [sector, setsector] = useState([{key: "", text: "", value: ""}])
    const [sectorValue, setsectorValue] = useState("")
    const [industry, setindustry] = useState([{key: "", text: "", value: ""}])
    const defaultValue = [{key: "", text: "", value: ""}]
    const [industryValue, setindustryValue] = useState("")
    const [companyCount, setcompanyCount] = useState(100)
    const [currentPage, setcurrentPage] = useState(1)
    const [companies, setcompanies] = useState([])

    const turnIntoOptions = (data,type) => {data[type].map(x=>({
        key: x["Name"],
        text: x["Name"],
        value: x["Name"],
    })) }

    const handlePageClick = ({target}) => {
        setcurrentPage(eval(target.textContent))
    }

    const handlePageNext = (operator)=>{
        setcurrentPage((prevState)=> {
            if((prevState > 1 || operator != "-")&& (prevState < companyCount/10 || operator == "-") ){
               return eval(prevState + operator + 1)
            }else{
                return prevState;
            }
            
        }
        )}
    
        const fetchCompanys = async (page = -1) => {
            const rawResponse = await fetch(`http://localhost:3010/api/Companies/IIS`, {
              method: 'POST',
              headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
              },
              body: JSON.stringify({SectorName : sectorValue,IndexName: indexValue,IndustryName: industryValue,Page:currentPage})
            });
            const content = await rawResponse.json();
            let companyInfo = JSON.parse(content);
            setcompanies(companyInfo["companyPoco"])
            setcompanyCount(companyInfo["Count"])
            if (page == -1){
                setcurrentPage(1);
            }
            };

        useEffect(() => {
            let timer1 = setTimeout(() => 
            fetchCompanys(-1)
            , 1000);
      
            // this will clear Timeout
            // when component unmount like in willComponentUnmount
            // and show will not change to true
            return () => {
              clearTimeout(timer1);
            };
          }, [indexValue,sectorValue,industryValue])

          useEffect(() => {
            let timer1 = setTimeout(() => 
                fetchCompanys(currentPage)
            , 1000);
      

            return () => {
              clearTimeout(timer1);
            };
          }, [currentPage])

        useEffect(() => {
            var data = fetch(`http://localhost:3010/api/Companies/industries/${sectorValue}`)
            .then(response => response.json());
            data.then(data => data.map(x=>({
                key: x["Name"],
                text: x["Name"],
                value: x["Name"],
            })) )
            .then(arrayFinal => setindustry([...defaultValue,...arrayFinal]))
        }, [sectorValue])

    useEffect(() => {
        try {        
            
            var data = fetch('http://localhost:3010/api/Companies/indexSector')
            .then(response => response.json());
            
            data.then(data => data["index"].map(x=>({
                key: x["Name"],
                text: x["Name"],
                value: x["Name"],
            })) )
            .then(arrayFinal => setindex((prevState) => [...prevState, ...arrayFinal]))

            data.then(data => data["sector"].map(x=>({
                key: x["Name"],
                text: x["Name"],
                value: x["Name"],
            })) )
            .then(arrayFinal => setsector((prevState) => [...prevState, ...arrayFinal]))
            console.log(index);
        } catch (error) {
            console.log(error)
        }
    }, [])

    console.log(companies)
    return (
        <Segment textAlign='left' className='segment'>
            <h1>List of Companies</h1>
            <Segment.Inline >
            <Dropdown placeholder='Index' onChange={(e)=>setindexValue(e.target.textContent)} search selection scrolling options={index}/>
            <Dropdown placeholder='Sector' onChange={(e)=>setsectorValue(e.target.textContent)} search selection scrolling options={sector}/>
            <Dropdown placeholder='Industry' onChange={(e)=>setindustryValue(e.target.textContent)} search selection scrolling options={industry}/>
            </Segment.Inline>
            
            <Table celled >
                <Table.Header>
                <Table.Row>
                    <Table.HeaderCell width="3">Ticker</Table.HeaderCell>
                    <Table.HeaderCell width="5">Company Name</Table.HeaderCell>
                    <Table.HeaderCell width="2">Score</Table.HeaderCell>
                    <Table.HeaderCell width="2">Sticker Price</Table.HeaderCell>
                    <Table.HeaderCell width="2">Previous Close</Table.HeaderCell>
                    <Table.HeaderCell>Profile</Table.HeaderCell>
                </Table.Row>
                </Table.Header>
            <Table.Body className="table">
                {
                    companies.map((x,i)=> <Company company={x} key={i}/>)
                }
            </Table.Body>
                <Table.Footer>
                <Table.Row>
                    <Table.HeaderCell colSpan='10'>
                    <Menu floated='right' pagination>
                        <Menu.Item onClick={()=>handlePageNext("-")} as='a' icon>
                        <Icon name='chevron left' />
                        </Menu.Item>
                        <Pagination count = {companyCount} page={currentPage} handlePageClick={handlePageClick}/>
                        <Menu.Item onClick={() => handlePageNext("+")} as='a' icon>
                        <Icon name='chevron right' />
                        </Menu.Item>
                    </Menu>
                    </Table.HeaderCell>
                    </Table.Row>
                </Table.Footer>
            </Table>
        </Segment>
            
        
    )
}
