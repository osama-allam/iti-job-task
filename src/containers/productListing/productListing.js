import React, { Component } from 'react';
import {Table, Button} from 'element-react/next';
import cssClasses from './productListing.module.css';

class ProductListing extends Component {
  constructor(props) {
    super(props);
    this.state = {
      columns: [
        {
          label: "Image",
          prop: "image",
          width: 250,
          align: "center",
          render: function(data){
            return (
            <span>
              <img src={data.Image} className={cssClasses.tableImage} alt="Product's"/>
            </span>)
          }
        },
        {
          label: "Name",
          prop: "name",
          width: 200,
          align: "center",
          render: function(data){
            return (<>{data.Name}</>);
          }
        },
      
        {
          label: "Price",
          prop: "price",
          width: 200,
          align: "center",
          render: function(data){
            return (<>{data.Price}</>);
          }
        },
        {
          label: "Producer",
          prop: "company",
          width: 200,
          align: "center",
          render: function(data){
            return (<>{data.Company.Name}</>);
          }
        },
        {
          label: "",
          width: 200,
          align: "center",
          render: (data) => {
            return (
              <span>
               <Button type="info" size="small" onClick={()=>{this.props.history.push(`products/edit/${data.Id}`)}}>Edit</Button>
              </span>
            )
          }
        }
      ],
      data: [{
        Id: 11,
        Image: 'https://d2pa5gi5n2e1an.cloudfront.net/global/images/product/laptops/Dell_Inspiron_N4050/Dell_Inspiron_N4050_L_1.jpg',
        Name: 'Dell',
        Price: 7000,
        Company: {Name: "ITI"}
      },
      {
        Id: 12,
        Image: 'https://pisces.bbystatic.com/image2/BestBuy_US/images/products/6212/6212601cv1d.jpg',
        Name: 'Acer',
        Price: 7000,
        Company: {Name: "ITI"}
      },
      // {
      //   Id: 13,
      //   Image: 'https://www.thestar.com.my/~/media/online/2018/11/30/13/52/macbookair_dpa.ashx/?w=620&h=413&crop=1&hash=0B2B481FFDB2B4FB0B375F5BB5FE1CD26DC408F6',
      //   Name: 'MacBook',
      //   Price: 7000,
      //   Company: {Name: "ITI"}
      // },
      // {
      //   Id: 14,
      //   Image: 'https://i5.walmartimages.com/asr/351418ea-2da6-4e89-bcf0-3b59d85dea4d_1.2d261da7177c7653c27785d84c8729d3.jpeg?odnHeight=450&odnWidth=450&odnBg=FFFFFF',
      //   Name: 'Lenovo',
      //   Price: 7000,
      //   Company: {Name: "ITI"}
      // },
      {
        Id: 15,
        Image: 'https://www.91-img.com/pictures/laptops/sony/sony-vpceh38fn-core-i5-2nd-gen-4-gb-500-gb-windows-7-1-gb-59712-large-1.jpg',
        Name: 'Vaio',
        Price: 7000,
        Company: {Name: "ITI"}
      }]
    }
  }
  
  tableStyle = {
    width: 'max-content',
    marginLeft: 'auto',
    fontWeight: 'bold'
  }
  render() {
    return (
      <Table
        style={this.tableStyle}
        columns={this.state.columns}
        data={this.state.data}
        border={true}
        highlightCurrentRow={true}
      />
    )
  }
}

export default ProductListing;
