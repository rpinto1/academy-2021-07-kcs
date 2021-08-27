
import _ from 'lodash'
import React, { useEffect, useState } from 'react'
import { Dropdown, Segment, Table ,Menu, Icon} from 'semantic-ui-react'
import { Company } from './Company'

export const IISList = () => {


    const [index, setindex] = useState([{key: "", text: "", value: ""}])
    const [indexValue, setindexValue] = useState("")
    const [sector, setsector] = useState([{key: "", text: "", value: ""}])
    const [sectorValue, setsectorValue] = useState("")
    const [industry, setindustry] = useState([{key: "", text: "", value: ""}])
    const [industryValue, setindustryValue] = useState("")
    const turnIntoOptions = (data,type) => {data[type].map(x=>({
        key: x["Name"],
        text: x["Name"],
        value: x["Name"],
    })) }

        useEffect(() => {
            var data = fetch(`http://localhost:3010/api/Companies/industries/${sectorValue}`)
            .then(response => response.json());
            data.then(data => data.map(x=>({
                key: x["Name"],
                text: x["Name"],
                value: x["Name"],
            })) )
            .then(arrayFinal => setindustry(arrayFinal))
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
            .then(arrayFinal => setindex(arrayFinal))

            data.then(data => data["sector"].map(x=>({
                key: x["Name"],
                text: x["Name"],
                value: x["Name"],
            })) )
            .then(arrayFinal => setsector(arrayFinal))
            console.log(index);
        } catch (error) {
            console.log(error)
        }
    }, [])



    var arrayCompanys = []
    for (let index = 0; index < 10; index++) {
        arrayCompanys.push({});
        
    }

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
                    <Table.HeaderCell width="8">Company</Table.HeaderCell>
                    <Table.HeaderCell width="2">Score</Table.HeaderCell>
                    <Table.HeaderCell width="2">Sticer Price</Table.HeaderCell>
                    <Table.HeaderCell width="2">Previous Close</Table.HeaderCell>
                    <Table.HeaderCell>Profile</Table.HeaderCell>
                </Table.Row>
                </Table.Header>
            <Table.Body className="table">
                {
                    arrayCompanys.map((x,i)=> <Company key ={i}/>)
                }
            </Table.Body>
                <Table.Footer>
                <Table.Row>
                    <Table.HeaderCell colSpan='5'>
                    <Menu floated='right' pagination>
                        <Menu.Item as='a' icon>
                        <Icon name='chevron left' />
                        </Menu.Item>
                        <Menu.Item as='a'>1</Menu.Item>
                        <Menu.Item as='a'>2</Menu.Item>
                        <Menu.Item as='a'>3</Menu.Item>
                        <Menu.Item as='a'>4</Menu.Item>
                        <Menu.Item as='a' icon>
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
