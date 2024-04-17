import { View, ScrollView, StyleSheet } from "react-native";
import Company from "./Company";
import Distributor from "./Distributor";
import Product from "./Product";

const ProductBlock = ({ products, onTogle, navigation }) => {
  return (
    <View style={styles.container}>
      <View style={styles.companyBlock}>
        <Company
          idCompany={products[0].product.idCompany}
          idDistributor={products[0].product.idDistributor}
        />
        <Distributor />
      </View>

      <ScrollView
        style={styles.productBlock}
        horizontal
        pagingEnabled
        showsHorizontalScrollIndicator={false}
      >
        {products.map((product, productIndex) => (
          <Product
            key={productIndex}
            product={product.product}
            onTogle={() => onTogle(product.product)}
            marginRight={productIndex % 2 === 0}
            lastChild={productIndex == products.length - 1}
            navigation={navigation}
          />
        ))}
      </ScrollView>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    marginBottom: 30,
  },
  companyBlock: {},
  productBlock: {
    flexGrow: 0,
    flexDirection: "row",
  },
});

export default ProductBlock;
