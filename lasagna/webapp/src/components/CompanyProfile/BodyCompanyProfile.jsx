import React, { useState } from 'react'
import { Radio, Container, Form, Table } from 'semantic-ui-react';
import CompanyTitleAndLink from './CompanyTitleAndLink';
import RuleOnegraph from './RuleOnegraph';



export default function BodyCompanyProfile({companyInfo}) {

    const ticker = companyInfo;

    const [selected, setSelected] = useState({
        ticker: ticker,
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
                                label='Return on Invested Capital'
                                value= 'ROIC' 
                                checked = {selected.option == 'ROIC'}
                                onChange={handleChange}/>
                            </Form.Field>
                            <Form.Field>
                                <Radio name='aspect'
                                label='Dividends per Share' 
                                value='DPS'
                                checked = {selected.option == 'DPS'}
                                onChange={handleChange}/>
                            </Form.Field>
                            <Form.Field>
                                <Radio name='aspect'
                                label='Gross Margin' 
                                value='GM'
                                checked = {selected.option == 'GM'}
                                onChange={handleChange}/>
                            </Form.Field>
                            <Form.Field>
                                <Radio name='aspect'
                                label='Gross Profit' 
                                value='GP'
                                checked = {selected.option == 'GP'}
                                onChange={handleChange}/>
                            </Form.Field>
                            <Form.Field>
                                <Radio name='aspect'
                                label='Operating Margin' 
                                value='OM'
                                checked = {selected.option == 'OM'}
                                onChange={handleChange}/>
                            </Form.Field>
                            <Form.Field>
                                <Radio name='aspect'
                                label='Operating profit' 
                                value='OP'
                                checked = {selected.option == 'OP'}
                                onChange={handleChange}/>
                            </Form.Field>
                            <Form.Field>
                                <Radio name='aspect'
                                label='Return on Assets' 
                                value='ROA'
                                checked = {selected.option == 'ROA'}
                                onChange={handleChange}/>
                            </Form.Field>
                            <Form.Field>
                                <Radio name='aspect'
                                label='Return on Equity' 
                                value='ROE'
                                checked = {selected.option == 'ROE'}
                                onChange={handleChange}/>
                            </Form.Field>
                            <Form.Field>
                                <Radio name='aspect'
                                label='Revenue'
                                value='R'
                                checked = {selected.option == 'R'}
                                onChange={handleChange}/>
                            </Form.Field>
                            <Form.Field>
                                <Radio name='aspect'
                                label='Revenue Growth' 
                                value= 'RG'
                                checked = {selected.option == 'RG'}
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