import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { Dropdown, Segment, Table ,Menu, Icon, Header} from 'semantic-ui-react'
import { Company } from './Company'
import Pagination from './Pagination'
import TableHeaderAuth from './TableHeaderAuth'
import { token , userId , headers } from '../UserManager';
import TableHeaderNormal from './TableHeaderNormal'
import {  useHistory } from 'react-router';
import axios from 'axios';
import {portfolioAddBulk } from '../../redux/portfoliosReducer';



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
const countriesPicked = useSelector(state => state.countries);
const portfolios = useSelector(state => state.portfolios);
//const [token, setToken] = useState("")
const history = useHistory();
const dispatch = useDispatch();
const turnIntoOptions = (data,type,type2) => {return data[type][type2].map(x=>({
    key: x["name"],
    text: x["name"],
    value: x["name"],
})) }

const handlePageClick = ({target}) => {
    setcurrentPage(parseInt(target.textContent))
}

const handlePageNext = (operator)=>{
    setcurrentPage((prevState)=> {
        if((prevState > 1 || operator !== "-")&& (prevState < companyCount/10 || operator === "-") ){
            return parseInt(prevState + operator + 1)
        }else{
            return prevState;
        }
        
    }
    )}


    const fetchCompanys = async (page = -1) => {

        const url = `http://localhost:3010/api/Portfolios/portfolio?userId=${userId}`;

        if(userId!==null){
            if(portfolios[0].portfolioCompanies[0] !== undefined ){
                if(portfolios[0].portfolioCompanies[0].portfolioId === undefined ){
                   
                        (() => axios.get(url, headers)
                        .then(res => {

                            dispatch(portfolioAddBulk(res.data.result));

                        })
                        .catch(error => console.log(error)))();
                
                        }
            }       }
            
         


        const fetchUrl = token===null?  `http://localhost:3010/api/Companies/IIS`:`http://localhost:3010/api/Companies/authenticated`;
        const rawResponse =fetch(fetchUrl, {
            method: 'POST',
            headers :  { 
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        }
            ,
            credentials: 'include',
            redirect: 'manual',
            body: JSON.stringify({Sectorname : sectorValue,
                                Indexname: indexValue,
                                Industryname: industryValue,
                                Page:currentPage,
                                Countries:countriesPicked
                                })
        }
        );

        console.log("---------------------" + JSON.stringify({Sectorname : sectorValue,
            Indexname: indexValue,
            Industryname: industryValue,
            Page:currentPage,
            Countries:countriesPicked
            }))


        rawResponse.then(response => {

            if(response.ok){
                console.log(response);
            return response.json();
            }else{
                 //history.push('signin');
            }
            
            
        }).then(data => {
            if(data == null){
                return;
            }
            const dataToUse = token===null? data["result"]["companyPocos"] : data.result.companyPocosAuthenticated
            setcompanies(dataToUse)
            setcompanyCount(data.result.count)
        })

        if (page === -1){
            setcurrentPage(1);
        }
        };

        const companyResults = ()=> {
            if(companies.length === 0){
                return(
                    <Table.Body className="table">
                        <Table.Row>
                            <Table.Cell colSpan={10}>
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
                <Table.Body className="table">
                {
                    companies.map((x,i)=> <Company portolios={portfolios} company={x} key={i}/>)
                }
                </Table.Body>
            )

        };
    
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
        }, [indexValue,sectorValue,industryValue,countriesPicked]);

        useEffect(() => {
        let timer1 = setTimeout(() => 
            fetchCompanys(currentPage)
        , 500);
    

        return () => {
            clearTimeout(timer1);
        };
        }, [currentPage]);

        useEffect(() => {
            var data = fetch(`http://localhost:3010/api/Companies/industries/${sectorValue}`)
            .then(response => response.json());
            data.then(data => data["result"].map(x=>({
                key: x["name"],
                text: x["name"],
                value: x["name"],
            })) )
            .then(arrayFinal => setindustry([...defaultValue,...arrayFinal]))
        }, [sectorValue]);

        useEffect(() => {
        /*     let sessionToken = sessionStorage.getItem("token");
            let localToken = localStorage.getItem("token");
            if(sessionToken == null){
                setToken(localToken);
            }else{
                setToken(sessionToken);
            } */
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
        }, []);


return (
    <Segment padded textAlign='left' className='segment'>
        <h1>List of Companies</h1>

        <Dropdown placeholder='Index' onChange={(e,data)=>setindexValue(data.value)} search selection scrolling options={index}/>
        <Dropdown style={{marginLeft:5}} placeholder='Sector' onChange={(e,data)=>setsectorValue(data.value)} search selection scrolling options={sector}/>
        <Dropdown style={{marginLeft:5}} placeholder='Industry' onChange={(e,data)=>setindustryValue(data.value)} search selection scrolling options={industry}/>

        
        <Table celled >
       { token===null? <TableHeaderNormal/> : <TableHeaderAuth />}
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
