import _ from 'lodash'
import React, { useEffect, useState } from 'react'
import { Dropdown, Segment, Table ,Menu, Icon} from 'semantic-ui-react'
import { Company } from './Company'

export const IISList = () => {

    const [index, setindex] = useState(["ok","ok2"])
    const [sector, setsector] = useState([()=>"ok","ok2"])
    const [industry, setindustry] = useState(["ok","ok2"])

    useEffect(() => {
    
    }, [])



    var arrayCompanys = []
    for (let index = 0; index < 10; index++) {
        arrayCompanys.push({});
        
    }

    return (
        <Segment textAlign='left' className='segment'>
            <h1>List of Companies</h1>
            <Segment.Inline >
            <Dropdown placeholder='Index' search selection scrolling options={()=>index}/>
            <Dropdown placeholder='Sector' search selection scrolling options={sector}/>
            <Dropdown placeholder='Industry' search selection scrolling options={industry}/>
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
                    arrayCompanys.map(x=> <Company />)
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
