import React, { useEffect, useState } from 'react'
import { useSelector } from 'react-redux'
import { Dropdown, Segment, Table ,Menu, Icon, Header} from 'semantic-ui-react'
import { Company } from './Company'
import Pagination from './Pagination'



export const IISList = () => {


    const [index, setindex] = useState([{key: "", text: "--None--", value: ""}])
    const [indexValue, setindexValue] = useState("")
    const [sector, setsector] = useState([{key: "", text: "--None--", value: ""}])
    const [sectorValue, setsectorValue] = useState("")
    const [industry, setindustry] = useState([{key: "", text: "--None--", value: ""}])
    const defaultValue = [{key: "", text: "--None--", value: ""}]
    const [industryValue, setindustryValue] = useState("")
    const [companyCount, setcompanyCount] = useState(0)
    const [currentPage, setcurrentPage] = useState(1)
    const [companies, setcompanies] = useState([])
    const countriesPicked = useSelector(state => state.countries)
    

    const turnIntoOptions = (data,type,type2) => {return data[type][type2].map(x=>({
        key: x["name"],
        text: x["name"],
        value: x["name"],
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
            const rawResponse = fetch(`http://localhost:3010/api/Companies/IIS`, {
              method: 'POST',
              headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
              },
              body: JSON.stringify({Sectorname : sectorValue,
                                    Indexname: indexValue,
                                    Industryname: industryValue,
                                    Page:currentPage,
                                    Countries:countriesPicked
                                    })
            });
            const content = rawResponse.then(response => response.json());
            content.then(data => {
                setcompanies(data["result"]["companyPocos"])
                setcompanyCount(data["result"]["count"])
            })

            if (page == -1){
                setcurrentPage(1);
            }
            };

        const companyResults = ()=> {
            if(companies.length == 0){
                return(
                    <Table.Body classname="table">
                        <Table.Row>
                            <Table.Cell colSpan={6}>
                            <Segment className="tableSegment" textAlign="center" size="small">
                                <Header icon>
                                <Icon name='search' />
                                We don't have any Companies matching your query.
                                </Header>
                            </Segment>
                            </Table.Cell>


                        </Table.Row>


                    </Table.Body>

                    )
            } 
            return(
                <Table.Body classname="table">
                {
                    companies.map((x,i)=> <Company company={x} key={i}/>)
                }
                </Table.Body>
            )

        }
        
        useEffect(() => {
            let timer1 = setTimeout(() => 
            fetchCompanys(-1)
            , 500);
      
            // this will clear Timeout
            // when component unmount like in willComponentUnmount
            // and show will not change to true
            return () => {
              clearTimeout(timer1);
            };
          }, [indexValue,sectorValue,industryValue,countriesPicked])

          useEffect(() => {
            let timer1 = setTimeout(() => 
                fetchCompanys(currentPage)
            , 500);
      

            return () => {
              clearTimeout(timer1);
            };
          }, [currentPage])

        useEffect(() => {
            var data = fetch(`http://localhost:3010/api/Companies/industries/${sectorValue}`)
            .then(response => response.json());
            data.then(data => data["result"].map(x=>({
                key: x["name"],
                text: x["name"],
                value: x["name"],
            })) )
            .then(arrayFinal => setindustry([...defaultValue,...arrayFinal]))
        }, [sectorValue])

    useEffect(() => {
        try {
            var data = fetch(`http://localhost:3010/api/Companies/indexSector`)
            .then(response => response.json());
            data.then(data => turnIntoOptions(data,"result","indices"))
            .then(arrayFinal => setindex((prevState) => [...prevState, ...arrayFinal]))

            data.then(data => turnIntoOptions(data,"result","sectors") )
            .then(arrayFinal => setsector((prevState) => [...prevState, ...arrayFinal]))
        } catch (error) {
            console.log(error)
        }
    }, [])


    return (
        <Segment textAlign='left' classname='segment'>
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
                    <Table.HeaderCell width="5">Company name</Table.HeaderCell>
                    <Table.HeaderCell width="2">Score</Table.HeaderCell>
                    <Table.HeaderCell width="2">Sticker Price</Table.HeaderCell>
                    <Table.HeaderCell width="2">Previous Close</Table.HeaderCell>
                    <Table.HeaderCell>Profile</Table.HeaderCell>
                </Table.Row>
                </Table.Header>
                {companyResults()}
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
