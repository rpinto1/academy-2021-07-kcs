import React, { useState } from 'react'
import { Radio, Container, Form, Table } from 'semantic-ui-react';
import CompanyTitleAndLink from './CompanyTitleAndLink';
import RuleOnegraph from './RuleOnegraph';



export default function BodyCompanyProfile({companyInfo}) {

    
    const [selected, setSelected] = useState({
        option: 'ROIC'
    })
          
    const handleChange = (event, {value}) => {
        setSelected({
            option : value
        });
    };

  

    return (
        <>
        <Container> 
            <CompanyTitleAndLink companyInfo={companyInfo}/>
        </Container>
        <Container>
        <Table definition>
            <Table.Body>
                <Table.Row>
                    <Table.Cell width={6}>
                    
                        <Form className = 'ui list' id="checkboxes-list" >

                            <Form.Field>
                                <Radio 
                                name='aspect'
                                label='ROIC'
                                value= 'ROIC' 
                                checked = {selected.option == 'ROIC'}
                                onChange={handleChange}/>
                            </Form.Field>
                            <Form.Field>
                                <Radio name='aspect'
                                label='Sales growth rate' 
                                value='SGR'
                                checked = {selected.option == 'SGR'}
                                onChange={handleChange}/>
                            </Form.Field>
                            <Form.Field>
                                <Radio name='aspect'
                                label='Earnings per share'
                                value='EPS'
                                checked = {selected.option == 'EPS'}
                                onChange={handleChange}/>
                            </Form.Field>
                            <Form.Field>
                                <Radio name='aspect'
                                label='Equity growth rate' 
                                value= 'BVPS'
                                checked = {selected.option == 'BVPS'}
                                onChange={handleChange}/>
                            </Form.Field>
                            <Form.Field>
                                <Radio name='aspect'
                                label='Cash Flow' 
                                value='FCF'
                                checked = {selected.option == 'FCF'}
                                onChange={handleChange}/>                            
                            </Form.Field>
                        </Form>
                    </Table.Cell>
                    <Table.Cell>
                        <RuleOnegraph selected={selected}/>
                     </Table.Cell>
                </Table.Row>
            </Table.Body>
        </Table>
                    
        </Container>            
                    
            
    
        </>
    )
}